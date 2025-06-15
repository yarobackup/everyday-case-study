using CompanyName.Level;
using UnityEngine;
namespace CompanyName.LevelLoaderService
{
  [CreateAssetMenu(fileName = "LevelDataScriptable", menuName = "Scriptable Objects/LevelDataScriptable")]
  public class LevelDataScriptable : ScriptableObject
  {
    public DailyData[] dailyData;
  }
}
