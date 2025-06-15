using CompanyName.Ui;
using UnityEngine;

namespace CompanyName.WindowManager
{
    public struct PopUpOpenOptions
    {
        public readonly string PopUpName { get; }
        public readonly bool CloseOtherWindows { get; }

        public PopUpOpenOptions(string popUpName, bool closeOtherWindows)
        {
            PopUpName = popUpName;
            CloseOtherWindows = closeOtherWindows;
        }
    }
    public interface IWindowManager
    {
        public void RegisterWindowPrefab(string windowId, UIWindow window);
        void UnregisterWindow(string windowID);
        void OpenPopUp<T>(T cfg, PopUpOpenOptions openOptions, WindowCallbacks callbacks = null) where T : IPopUpOptions;
        void OpenWindow(string windowID, MonoBehaviour context, WindowOptions options = null, WindowCallbacks callbacks = null);
        void CloseTopWindow(WindowOptions options = null, WindowCallbacks callbacks = null);
        void CloseAllWindows();
    }
}