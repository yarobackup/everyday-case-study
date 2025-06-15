using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CompanyName.Game.Colorsort
{
  public class ColorsortColumnItem : MonoBehaviour
  {
    [SerializeField] private ColorsortBlockItem _blockItemPrefab;
    [SerializeField] private RectTransform _itemsContainer;
    [SerializeField] private Button _button;
    private List<ColorsortBlockItem> _blocks;

    private Action<int> _onColumnClick;

    public RectTransform Rt => transform as RectTransform;

    private int _index;
    private int _height;
    internal void Init(int index, int height, List<ColorsortItem> items, Action<int> onColumnClick)
    {
      _index = index;
      _height = height;
      _onColumnClick = onColumnClick;
      _blocks = new List<ColorsortBlockItem>();
      for (int i = 0; i < items.Count; i++)
      {
        var blockItem = Instantiate(_blockItemPrefab, _itemsContainer);
        blockItem.Init(items[i]);
        _blocks.Add(blockItem);
      }
      CalculatePositions();
      CalculateHeight(height);
    }

    private void CalculateHeight(int height)
    {
      var blockHeight = _blockItemPrefab.Rt.rect.height;
      var totalHeight = height * blockHeight;
      Rt.sizeDelta = new Vector2(Rt.sizeDelta.x, totalHeight);
    }

    private void CalculatePositions()
    {
      var blockHeight = _blockItemPrefab.Rt.rect.height;
      for (int i = 0; i < _blocks.Count; i++)
      {
        _blocks[i].Rt.anchoredPosition = new Vector2(0, i * blockHeight);
      }
    }

    private void MarkAsSolved(bool isSolved)
    {
      _button.interactable = !isSolved;
      for (int i = 0; i < _blocks.Count; i++)
      {
        _blocks[i].SetSolved(isSolved);
      }
    }

    internal void Subscribe()
    {
      _button.onClick.AddListener(OnColumnClick);
    }

    private void OnColumnClick()
    {
      _onColumnClick?.Invoke(_index);
    }

    internal void Unsubscribe()
    {
      _button.onClick.RemoveAllListeners();
    }

    internal void Select(int peekCount)
    {
      var blockHeight = _blockItemPrefab.Rt.rect.height;
      var columnHeight = Rt.rect.height;

      int startIndex = Mathf.Max(0, _blocks.Count - peekCount);
      for (int i = startIndex; i < _blocks.Count; i++)
      {
        var relativeIndex = i - startIndex;
        _blocks[i].Rt.anchoredPosition = new Vector2(0, columnHeight + relativeIndex * blockHeight);
      }
    }

    internal bool AddToTop(ColorsortBlockItem[] items, bool isSolved)
    {
      if (_blocks.Count + items.Length > _height)
      {
        return false;
      }
      for (int i = 0; i < items.Length; i++)
      {
        _blocks.Add(items[i]);
        items[i].transform.SetParent(_itemsContainer);
      }
      CalculatePositions();
      MarkAsSolved(isSolved);
      return true;
    }

    internal ColorsortBlockItem[] RemoveFromTop(int peekCount)
    {
      peekCount = Mathf.Min(peekCount, _blocks.Count);
      var result = new ColorsortBlockItem[peekCount];

      for (int i = 0; i < peekCount; i++)
      {
        int blockIndex = _blocks.Count - 1 - i;
        result[i] = _blocks[blockIndex];
      }

      _blocks.RemoveRange(_blocks.Count - peekCount, peekCount);
      for (int i = 0; i < _blocks.Count; i++)
      {
        _blocks[i].Refresh();
      }
      CalculatePositions();
      return result;
    }

    internal void Unselect()
    {
      CalculatePositions();
    }
  }
}