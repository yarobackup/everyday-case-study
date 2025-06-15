using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

namespace CompanyName.Ui
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PopUpCloseButton : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        private Button _button;

        private void EnableVisible(bool isVisible)
        {
            _canvasGroup.alpha = isVisible ? 1 : 0;
            _canvasGroup.interactable = isVisible;
            _canvasGroup.blocksRaycasts = isVisible;
        }

        private void SetEnabled(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        private void ShowDelayed(float delay)
        {
            var sequence = Sequence.Create();
            sequence.ChainDelay(delay);
            sequence.Chain(Tween.Alpha(_canvasGroup, 1, 0.3f));
            sequence.ChainCallback(() =>
            {
                EnableVisible(true);
            }, false);
        }

        internal void TryShowCloseButton(CloseButtonOptions closeButtonOptions)
        {
            if (closeButtonOptions != null && closeButtonOptions.IsButtonActive && closeButtonOptions.IsButtonDelayed)
            {
                ShowDelayed(closeButtonOptions.Delay);
            }
        }
        internal void ConfigureCloseButton(CloseButtonOptions closeButtonOptions)
        {
            if (closeButtonOptions == null || !closeButtonOptions.IsButtonActive)
            {
                EnableVisible(false);
                SetEnabled(false);
            }
            else
            {
                EnableVisible(!closeButtonOptions.IsButtonDelayed);
                SetEnabled(closeButtonOptions.IsButtonActive);
            }
        }

        internal void Unsubscribe()
        {
            _button.onClick.RemoveAllListeners();
        }

        internal void TrySubscribe(CloseButtonOptions closeButtonOptions)
        {
            if (closeButtonOptions == null || !closeButtonOptions.IsButtonActive)
            {
                return;
            }
            var handler = closeButtonOptions.BtnHandler;
            _button.onClick.AddListener(() =>
            {
                handler.Invoke();
            });
        }
    }

}
