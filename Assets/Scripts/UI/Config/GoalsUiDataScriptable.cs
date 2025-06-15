using System;
using CompanyName.Game;
using UnityEngine;

namespace CompanyName.Ui
{
  [CreateAssetMenu(fileName = "GoalsUiDataScriptable", menuName = "Scriptable Objects/Ui/GoalsUiDataScriptable")]
  public class GoalsUiDataScriptable : ScriptableObject
  {
    [SerializeField] private GoalUiSprite[] _goalUiSprites;
    public Sprite GetSprite(GoalType goalType)
    {
      for (int i = 0; i < _goalUiSprites.Length; i++)
      {
        if (_goalUiSprites[i].goalType == goalType)
        {
          return _goalUiSprites[i].Sprite;
        }
      }
      return null;
    }
  }

  [Serializable]
  public class GoalUiSprite
  {
    public GoalType goalType;
    public Sprite Sprite;
  }
}
