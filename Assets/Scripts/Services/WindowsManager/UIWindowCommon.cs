using System;
using CompanyName.Services.SafeArea;
using CompanyName.Services.SL;
using UnityEngine;

namespace CompanyName.WindowManager
{
    [RequireComponent(typeof(WindowAnimator))]
    public class UIWindowCommon : MonoBehaviour
    {
        [SerializeField] private RectTransform _rt;
        [SerializeField] private WindowAnimator _animator;

        [Header("Animation Settings")]
        [SerializeField] private WindowAnimationType _animationTypeShow = WindowAnimationType.Fade;
        [SerializeField] private float _animationDurationShow = 0.3f;
        [SerializeField] private WindowAnimationType _animationTypeHide = WindowAnimationType.None;
        [SerializeField] private float _animationDurationHide = 0.2f;

        private float AnimationDuration(float? overrideDuration, bool isShow)
        {
#if DEBUG_UI_MANAGER
            return 5 * (overrideDuration ?? (isShow ? _animationDurationShow : _animationDurationHide));
#else
            return overrideDuration ?? (isShow ? _animationDurationShow : _animationDurationHide);
#endif
        }

        private WindowAnimationType AnimationType(WindowAnimationType? overrideAnimationType, bool isShow)
        {
            return overrideAnimationType ?? (isShow ? _animationTypeShow : _animationTypeHide);
        }

        protected virtual void Awake()
        {
            ApplySafeArea();
        }
        internal void Animate(bool isShow, WindowOptions options, Action callback)
        {
            var animType = AnimationType(options.AnimationType, isShow);
            float duration = AnimationDuration(options.AnimationDuration, isShow);

            _animator.Animate(animType, isShow, duration, callback);
        }

        private void ApplySafeArea()
        {
            if (!_rt)
            {
                return;
            }

            this.GetService<SafeAreaService>(out var service);
            _rt.offsetMax = service.OffsetMax;
            _rt.offsetMin = service.OffsetMin;
        }
    }
}