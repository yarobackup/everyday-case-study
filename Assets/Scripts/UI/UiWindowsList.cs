using System;
using System.Collections.Generic;
using CompanyName.WindowManager;
using UnityEngine;

[CreateAssetMenu(fileName = "UiWindowsList", menuName = "Ui/UiWindowsList")]
public class UiWindowsList : ScriptableObject
{

  public List<WindowData> _windows;

  public List<WindowData> Windows => _windows;

  void OnValidate()
  {
    foreach (var window in _windows)
    {
      if (!window.windowPrefab)
      {
        Debug.LogError($"Window with {(string.IsNullOrEmpty(window.windowId) ? "empty id" : $"id {window.windowId}")} has no prefab assigned. File: {name}");
        return;
      }
      if (string.IsNullOrEmpty(window.windowId))
      {
        window.windowId = window.windowPrefab.WindowID;
      }
    }
  }
}

[Serializable]
public class WindowData
{
  public string windowId;
  public UIWindow windowPrefab;
}
