using System;
using System.Collections;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

namespace CompanyName.WindowManager
{
    [RequireComponent(typeof(CanvasGroup))]
    public class WindowAnimator : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _content;
        [SerializeField] private Vector3 _minScale = Vector3.one * 0.7f;

        private Sequence? _currentAnimation;

        private RectTransform _animatedTransform;
        private Vector3 _originalScale;

        private void Awake()
        {
            _animatedTransform = _content ? _content : transform as RectTransform;
            _originalScale = _animatedTransform.localScale;
        }

        public void Animate(WindowAnimationType animationType, bool isOpening, float duration, Action onComplete)
        {
            if (_currentAnimation.HasValue)
            {
                _currentAnimation.Value.Stop();
                _currentAnimation = null;
            }

            gameObject.SetActive(true);

            void ProceedWithNoAnimation()
            {
                _canvasGroup.alpha = isOpening ? 1f : 0f;
                _canvasGroup.blocksRaycasts = isOpening;

                // If closing, disable the game object
                if (!isOpening)
                {
                    gameObject.SetActive(false);
                }

                // Immediately invoke completion callback
                onComplete?.Invoke();
            }


            // Make sure the CanvasGroup blocks raycasts during animation
            _canvasGroup.blocksRaycasts = isOpening;

            // Start the appropriate animation based on type
            switch (animationType)
            {
                case WindowAnimationType.Fade:
                    _currentAnimation = FadeAnimation(_canvasGroup, isOpening, duration, onComplete);
                    break;
                case WindowAnimationType.Scale:
                    _currentAnimation = ScaleAnimation(_canvasGroup, isOpening, duration, onComplete);
                    break;
                case WindowAnimationType.SlideUp:
                    _currentAnimation = SlideUpAnimation(_canvasGroup, isOpening, duration, onComplete);
                    break;
                case WindowAnimationType.Custom:
                    // _currentAnimation = CustomAnimation(canvasGroup, isOpening, duration, onComplete);
                    break;
                default:
                    ProceedWithNoAnimation();
                    break;
            }
        }

        private Sequence FadeAnimation(CanvasGroup canvasGroup, bool isOpening, float duration, Action onComplete)
        {
            float startAlpha = isOpening ? 0f : 1f;
            float endAlpha = isOpening ? 1f : 0f;

            canvasGroup.alpha = startAlpha;

            var seq = Sequence.Create();
            seq.Chain(Tween.Alpha(canvasGroup, endAlpha, duration, Ease.OutCubic));
            seq.ChainCallback(() =>
            {
                canvasGroup.interactable = isOpening;
                if (!isOpening)
                {
                    gameObject.SetActive(false);
                }
                onComplete?.Invoke();
            });
            return seq;

        }

        private Sequence ScaleAnimation(CanvasGroup canvasGroup, bool isOpening, float duration, Action onComplete)
        {
            var startScale = isOpening ? _minScale : _originalScale;
            var endScale = isOpening ? _originalScale : _minScale;
            float endAlpha = isOpening ? 1f : 0f;

            canvasGroup.alpha = isOpening ? 0f : 1f;
            _animatedTransform.localScale = startScale;

            var seq = Sequence.Create();
            seq.Chain(Tween.Scale(_animatedTransform, endScale, duration, Ease.OutBack));
            seq.Group(Tween.Alpha(canvasGroup, endAlpha, duration, Ease.OutCubic));

            seq.ChainCallback(() =>
            {
                canvasGroup.interactable = isOpening;
                if (!isOpening)
                {
                    gameObject.SetActive(false);
                }
                onComplete?.Invoke();
            });
            return seq;
        }

        private Sequence SlideUpAnimation(CanvasGroup canvasGroup, bool isOpening, float duration, Action onComplete)
        {
            float endAlpha = isOpening ? 1f : 0f;
            var startPosition = isOpening ? _animatedTransform.rect.height / 2 : 0;
            var endPosition = isOpening ? 0 : _animatedTransform.rect.height / 2;

            canvasGroup.alpha = isOpening ? 0f : 1f;
            _animatedTransform.anchoredPosition = new Vector2(_animatedTransform.anchoredPosition.x, startPosition);

            var seq = Sequence.Create();
            seq.Chain(Tween.UIAnchoredPositionY(_animatedTransform, endPosition, duration, isOpening ? Ease.OutBack : Ease.InBack));
            seq.Group(Tween.Alpha(canvasGroup, endAlpha, isOpening ? duration : duration / 5, Ease.OutCubic, startDelay: isOpening ? 0 : 4 * duration / 5));

            seq.ChainCallback(() =>
            {
                canvasGroup.interactable = isOpening;
                if (!isOpening)
                {
                    gameObject.SetActive(false);
                }
                onComplete?.Invoke();
            });
            return seq;
        }

        protected virtual IEnumerator CustomAnimation(CanvasGroup canvasGroup, bool isOpening, float duration, Action onComplete)
        {
            yield return StartCoroutine(FadeAnimation(canvasGroup, isOpening, duration, onComplete));
        }
    }
}