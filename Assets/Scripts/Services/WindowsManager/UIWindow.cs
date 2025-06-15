using System;
using UnityEngine;

namespace CompanyName.WindowManager
{
    [RequireComponent(typeof(UIWindowCommon))]
    public abstract class UIWindow : MonoBehaviour
    {
        [SerializeField] private UIWindowCommon _common;

        protected MonoBehaviour Context { get; private set; }

        // State
        private bool _isShown;
        private bool _isSubscribed;
        private bool _isAnimating;

        // Properties
        public abstract string WindowID { get; }
        public bool IsShown => _isShown;
        public bool IsSubscribed => _isSubscribed;
        public bool IsAnimating => _isAnimating;

        protected virtual void Awake()
        {
            OnAwakeAdHoc();
        }

        protected virtual void OnAwakeAdHoc()
        {

        }

        public void SetContext(MonoBehaviour context)
        {
            Context = context;
            OnSetContextAdHoc();
        }

        protected virtual void OnSetContextAdHoc()
        {
        }


        public virtual void Show(WindowOptions options = null, WindowCallbacks callbacks = null)
        {
            if (_isShown || _isAnimating)
            {
                callbacks?.OnError?.Invoke();
                return;
            }

            options ??= new WindowOptions();

            _isAnimating = true;
            OnShowStart();
            callbacks?.OnStart?.Invoke();

            _common.Animate(true, options, () =>
            {
                _isShown = true;
                _isAnimating = false;
                Subscribe();
                OnShowEnd();
                callbacks?.OnEnd?.Invoke();
            });
        }

        protected virtual void OnShowStart()
        {

        }

        protected virtual void OnShowEnd()
        {

        }

        public virtual void Unsubscribe()
        {
        }

        public virtual void Subscribe()
        {
        }

        protected virtual void OnWindowHideCompleted() { }

        internal void Hide(WindowOptions options, WindowCallbacks callbacks)
        {
            if (!_isShown || _isAnimating)
            {
                callbacks?.OnError?.Invoke();
                return;
            }

            options ??= new WindowOptions();

            _isAnimating = true;
            Unsubscribe();
            callbacks?.OnStart?.Invoke();

            _common.Animate(false, options, () =>
            {
                _isShown = false;
                _isAnimating = false;
                OnWindowHideCompleted();
                callbacks?.OnEnd?.Invoke();
            });
        }
    }

    public enum WindowAnimationType
    {
        None,
        Fade,
        Scale,
        SlideUp,
        Custom
    }

    public class WindowCallbacks
    {
        public Action OnStart;
        public Action OnError;
        public Action OnEnd;
    }
    public class WindowOptions
    {
        public bool IsPopUp { get; set; } = false;
        public bool CloseOtherWindows { get; set; } = false;

        public WindowAnimationType? AnimationType { get; set; } = null;

        public float? AnimationDuration { get; set; } = null;
    }
}