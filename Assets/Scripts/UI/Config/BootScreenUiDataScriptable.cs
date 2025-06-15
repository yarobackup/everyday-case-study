using UnityEngine;

namespace CompanyName.Ui
{
  [CreateAssetMenu(fileName = "BootScreenUiDataScriptable", menuName = "Scriptable Objects/Ui/BootScreenUiDataScriptable")]
  public class BootScreenUiDataScriptable : ScriptableObject
  {
    [SerializeField]
    private float _minWaitTimeDebug = 1f;

    [SerializeField]
    private float _minWaitTimeRelease = 3f;

    public float MinWaitTime
    {
      get
      {
#if UNITY_EDITOR
        return _minWaitTimeDebug;
#else
        return _minWaitTimeRelease;
#endif
      }
    }
  }
}
