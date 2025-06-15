using System;
using CompanyName.Services.SL;
using CompanyName.Ui;
using CompanyName.WindowManager;
using NUnit.Framework;
using UnityEngine;

namespace CompanyName
{
    [DefaultExecutionOrder(-150)]
    public abstract class BaseSceneContext<TView, TPresenter> : MonoBehaviour where TView : ScreenBase<TPresenter> where TPresenter : class, IPresenter
    {
        [SerializeField]
        private TView _defaultUiWindow;

        [SerializeField]
        private UiWindowsList _windowsList;

        [SerializeField]
        private SceneServiceInstallerBase[] _sceneServices;

        protected abstract string DefaultUiWindowId { get; }

        protected bool HasDefaultUiWindow => _defaultUiWindow != null && !string.IsNullOrEmpty(DefaultUiWindowId);

        protected IWindowManager WindowManager;
        protected TPresenter _presenter;

        protected virtual void Awake()
        {
            WindowManager = ServiceLocator.Global.Get<IWindowManager>();
            TryRegisterSceneServices();
            AwakeAdHoc();
        }

        private void TryRegisterSceneServices()
        {
            if (_sceneServices != null)
            {
                for (int i = 0; i < _sceneServices.Length; i++)
                {
                    Assert.IsNotNull(_sceneServices[i], $"Scene service {i} is null");
                    _sceneServices[i].Register();
                }
            }
        }

        private void TryInitSceneServices()
        {
            if (_sceneServices != null)
            {
                for (int i = 0; i < _sceneServices.Length; i++)
                {
                    Assert.IsNotNull(_sceneServices[i], $"Scene service {i} is null");
                    _sceneServices[i].Init();
                }
            }
        }

        protected abstract TPresenter CreatePresenter();

        protected virtual void AwakeAdHoc()
        {
            TryInitSceneServices();
            this.RegisterAsSceneService(_presenter = CreatePresenter());
            _presenter.Init();
            RegisterDefaultWindow();
            OpenDefaultWindow();
        }

        private void Start()
        {
            StartAdHoc();
        }

        protected virtual void StartAdHoc()
        {
            RegisterWindows();
            _presenter.OnStart();
        }

        private void OnDestroy()
        {
            DestroyAdHoc();
        }

        protected virtual void DestroyAdHoc()
        {
            UnregisterDefaultWindow();
            UnregisterWindows();
        }

        private void OpenDefaultWindow()
        {
            if (!HasDefaultUiWindow)
            {
                return;
            }
            var options = new WindowOptions();
            options.AnimationType = WindowAnimationType.None;
            options.CloseOtherWindows = true;
            WindowManager.OpenWindow(DefaultUiWindowId, this, options);
        }

        private void RegisterDefaultWindow()
        {
            if (HasDefaultUiWindow)
            {
                WindowManager.RegisterWindowPrefab(DefaultUiWindowId, _defaultUiWindow);
            }

        }

        private void UnregisterDefaultWindow()
        {
            if (HasDefaultUiWindow)
            {
                WindowManager.UnregisterWindow(DefaultUiWindowId);
            }
        }

        protected virtual void RegisterWindows()
        {
            if (!_windowsList)
            {
                return;
            }
            foreach (var window in _windowsList.Windows)
            {
                WindowManager.RegisterWindowPrefab(window.windowId, window.windowPrefab);
            }
        }

        protected virtual void UnregisterWindows()
        {
            if (!_windowsList)
            {
                return;
            }
            foreach (var window in _windowsList.Windows)
            {
                WindowManager.UnregisterWindow(window.windowId);
            }
        }
    }
}
