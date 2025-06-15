using System;
using System.Collections.Generic;
using CompanyName.Services.SL;
using CompanyName.Ui;
using UnityEngine;

namespace CompanyName.WindowManager
{
    public class WindowManager : MonoBehaviour, IWindowManager
    {
        [SerializeField]
        private RectTransform _windowContainer;

        private readonly Stack<UIWindow> _openWindows = new Stack<UIWindow>();
        private readonly HashSet<string> _openWindowIds = new HashSet<string>();

        private readonly Dictionary<string, UIWindow> _registeredWindows = new Dictionary<string, UIWindow>();

        public void RegisterWindowPrefab(string windowId, UIWindow window)
        {
            if (!_registeredWindows.ContainsKey(windowId))
            {
                _registeredWindows.Add(windowId, window);
            }
            else
            {
                Debug.LogWarning($"Window with ID '{windowId}' is already registered.");
            }
        }

        public void UnregisterWindow(string windowID)
        {
            if (_registeredWindows.ContainsKey(windowID))
            {
                _registeredWindows.Remove(windowID);
            }
        }

        public void OpenWindow(string windowID, MonoBehaviour context, WindowOptions options = null, WindowCallbacks callbacks = null)
        {
            if (_registeredWindows.TryGetValue(windowID, out UIWindow window))
            {
                OpenWindow(windowID, window, context, options, callbacks);
            }
            else
            {
                Debug.LogWarning($"Window with ID '{windowID}' is not registered.");
                callbacks?.OnError?.Invoke();
            }
        }

        private void OpenWindow(string windowID, UIWindow window, MonoBehaviour context, WindowOptions options = null, WindowCallbacks callbacks = null)
        {
            if (_openWindowIds.Contains(windowID))
            {
                Debug.LogWarning($"Window with ID '{windowID}' is already open.");
                callbacks?.OnError?.Invoke();
                return;
            }
            var isPopUp = options != null && options.IsPopUp;
            if (options != null && options.CloseOtherWindows)
            {
                CloseAllWindows();
            }

            if (_openWindows.TryPeek(out var oldTopWindow))
            {
                if (isPopUp)
                {
                    oldTopWindow.Unsubscribe();
                }
                else
                {
                    // oldTopWindow.Hide(new WindowOptions { AnimationType = WindowAnimationType.None });
                }
            }

            // Instantiate the window prefab
            var windowInstance = Instantiate(window, _windowContainer);
            windowInstance.transform.localScale = Vector3.one;
            windowInstance.SetContext(context);

            // Add to the open windows stack
            _openWindows.Push(windowInstance);
            _openWindowIds.Add(windowID);

            windowInstance.Show(options, callbacks);
        }

        public void CloseTopWindow(WindowOptions options = null, WindowCallbacks callbacks = null)
        {
            if (_openWindows.TryPeek(out var topWindow))
            {
                CloseWindow(topWindow.WindowID, topWindow, options, callbacks);
            }
            else
            {
                callbacks?.OnError?.Invoke();
            }
        }

        private void CloseWindow(string windowID, UIWindow window, WindowOptions options, WindowCallbacks callbacks)
        {
            if (!window.IsShown || window.IsAnimating)
            {
                callbacks?.OnError?.Invoke();
                return;
            }

            var wrappedCallbacks = new WindowCallbacks();
            wrappedCallbacks.OnError = callbacks?.OnError;
            wrappedCallbacks.OnStart = callbacks?.OnStart;
            wrappedCallbacks.OnEnd = () =>
            {
                if (window.gameObject)
                {
                    Destroy(window.gameObject);
                }

                var tempList = new List<UIWindow>(_openWindows);
                tempList.Remove(window);

                _openWindows.Clear();
                _openWindowIds.Remove(windowID);
                foreach (var w in tempList)
                {
                    _openWindows.Push(w);
                }

                if (_openWindows.TryPeek(out var topWindow))
                {
                    if (topWindow.IsShown)
                    {
                        // Open after pop up
                        topWindow.Subscribe();
                    }
                    else
                    {
                        // Open after window
                        var noAnimOptions = new WindowOptions { AnimationType = WindowAnimationType.None };
                        topWindow.Show(noAnimOptions, callbacks);
                    }
                }
                callbacks?.OnEnd?.Invoke();
            };
            window.Hide(options, wrappedCallbacks);

        }

        public void CloseAllWindows()
        {
            var options = new WindowOptions { AnimationType = WindowAnimationType.None };
            while (_openWindows.TryPop(out var w))
            {
                var callbacks = new WindowCallbacks()
                {
                    OnEnd = () =>
                    {
                        if (w.gameObject)
                        {
                            Destroy(w.gameObject);
                        }
                    }
                };
                w.Hide(options, callbacks);
            }
            _openWindowIds.Clear();
        }
        public void OpenPopUp<T>(T cfg, PopUpOpenOptions openOptions, WindowCallbacks callbacks = null) where T : IPopUpOptions
        {
            var obj = new GameObject("PopUp_SL").AddComponent<ServiceLocator>();
            var inner = new GameObject("PopUpController").AddComponent<PopUpController>();
            inner.transform.SetParent(obj.transform);
            obj.RegisterAsObjectService(cfg);

            var wrappedCallbacks = new WindowCallbacks
            {
                OnError = callbacks?.OnError,
                OnStart = callbacks?.OnStart,
                OnEnd = () =>
                    {
                        if (obj)
                        {
                            Destroy(obj.gameObject);
                        }
                        callbacks?.OnEnd?.Invoke();
                    }
            };

            OpenWindow(openOptions.PopUpName, inner, new WindowOptions { IsPopUp = true, CloseOtherWindows = openOptions.CloseOtherWindows }, wrappedCallbacks);
        }
    }
}