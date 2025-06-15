using CompanyName.Services.SL;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CompanyName
{
  [DefaultExecutionOrder(-200)]
  public class GlobalContext : MonoBehaviour
  {
    [SerializeField]
    private EventSystem _eventSystem;

    [SerializeField]
    private ServiceInstallerBase[] _globalServices;

    private void Awake()
    {
      Application.targetFrameRate = 60;
      Screen.sleepTimeout = SleepTimeout.NeverSleep;

      TryRegisterSceneServices();
      TryInitSceneServices();
    }

    private void TryRegisterSceneServices()
    {
      if (_globalServices != null)
      {
        for (int i = 0; i < _globalServices.Length; i++)
        {
          Assert.IsNotNull(_globalServices[i], $"Global service {i} is null");
          _globalServices[i].Register();
        }
      }
    }

    private void TryInitSceneServices()
    {
      if (_globalServices != null)
      {
        for (int i = 0; i < _globalServices.Length; i++)
        {
          Assert.IsNotNull(_globalServices[i], $"Global service {i} is null");
          _globalServices[i].Init();
        }
      }
    }

    private void Start()
    {
      DontDestroyOnLoad(_eventSystem.gameObject);
    }
  }
}