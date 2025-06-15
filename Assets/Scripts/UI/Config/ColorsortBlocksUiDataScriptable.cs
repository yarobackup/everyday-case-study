using System;
using CompanyName.Game.Colorsort;
using UnityEngine;

namespace CompanyName.Ui
{
  [CreateAssetMenu(fileName = "ColorsortBlocksUiDataScriptable", menuName = "Scriptable Objects/Ui/ColorsortBlocksUiDataScriptable")]
  public class ColorsortBlocksUiDataScriptable : ScriptableObject
  {
    [SerializeField] private ColorsortBlockUiSprite[] _goalUiSprites;
    [SerializeField] private Sprite _unknown;
    public Sprite GetSprite(ColorsortItemType type)
    {
      for (int i = 0; i < _goalUiSprites.Length; i++)
      {
        if (_goalUiSprites[i].blockType == type)
        {
          return _goalUiSprites[i].Sprite;
        }
      }
      return null;
    }

    public Sprite GetUnknownSprite()
    {
      return _unknown;
    }
  }

  [Serializable]
  public class ColorsortBlockUiSprite
  {
    public ColorsortItemType blockType;
    public Sprite Sprite;
  }
}
