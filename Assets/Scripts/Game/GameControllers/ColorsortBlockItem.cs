
using System;
using CompanyName.Ui;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CompanyName.Game.Colorsort
{
  public class ColorsortBlockItem : MonoBehaviour
  {
    [SerializeField] private ColorsortBlocksUiDataScriptable _sprites;
    [SerializeField] private Image _image;
    private ColorsortItem _item;

    private ColorsortItemType _type;

    public RectTransform Rt => transform as RectTransform;

    internal void Init(ColorsortItem item)
    {
      _item = item;
      Refresh();
    }

    internal void Refresh()
    {
      if (_item != null)
      {
        _type = _item.Type;
        if (_item.IsUnknown)
        {
          _image.sprite = _sprites.GetUnknownSprite();
        }
        else
        {
          UpdateByType();
        }
      }
    }

    internal void SetSolved(bool isSolved)
    {
      _image.color = new Color(1, 1, 1, isSolved ? 0.5f : 1);
    }

    private void UpdateByType()
    {
      _image.sprite = _sprites.GetSprite(_type);
    }
  }
}