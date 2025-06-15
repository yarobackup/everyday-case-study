using System;
using System.Collections.Generic;

namespace CompanyName.Game.Colorsort
{
  public class ColorsortColumn
  {
    private int _height;
    private bool _isSolved;
    private List<ColorsortItem> _items;

    public int Height => _height;
    public bool IsSolved => _isSolved;
    public List<ColorsortItem> Items => _items;
    public ColorsortItem TopItem => _items[_items.Count - 1];
    public bool IsSolvedOrEmpty => _isSolved || _items.Count == 0;

    public int FreeSpace => _height - _items.Count;

    public ColorsortColumn(int height)
    {
      _height = height;
      _items = new List<ColorsortItem>();
    }

    public bool AddToTop(ColorsortItem item)
    {
      if (_items.Count >= _height || _isSolved)
      {
        return false;
      }
      _items.Add(item);
      UpdateSolvedStatus();
      return true;
    }

    private void UpdateSolvedStatus()
    {
      _isSolved = CalculateSolvedStatus();
    }

    private bool CalculateSolvedStatus()
    {
      if (_items.Count != _height)
      {
        return false;
      }
      var topElement = _items[0];
      for (int i = 0; i < _items.Count; i++)
      {
        if (_items[i].IsUnknown)
        {
          return false;
        }
        if (_items[i].Type != topElement.Type)
        {
          return false;
        }
      }
      return true;
    }

    public ColorsortItem RemoveFromTop()
    {
      if (IsSolvedOrEmpty)
      {
        return null;
      }
      var item = _items[_items.Count - 1];
      _items.RemoveAt(_items.Count - 1);
      if (_items.Count > 0)
      {
        var newTopElement = _items[_items.Count - 1];
        if (newTopElement.IsUnknown)
        {
          newTopElement.IsUnknown = false;
        }
      }
      UpdateSolvedStatus();
      return item;
    }

    public int PeekFromColumn()
    {
      if (IsSolvedOrEmpty)
      {
        return 0;
      }
      var counter = 0;
      for (int i = _items.Count - 1; i >= 0; i--)
      {
        if (_items[i].IsUnknown)
        {
          return counter;
        }
        if (_items[i].Type == _items[_items.Count - 1].Type)
        {
          counter++;
        }
        else
        {
          return counter;
        }
      }
      return counter;
    }

    public bool CanPutToColumn(ColorsortItemType type)
    {
      if (_isSolved)
      {
        return false;
      }

      var isColorMatch = true;
      if (_items.Count == 0)
      {
        isColorMatch = true;
      }
      else
      {
        var topItem = _items[_items.Count - 1];
        if (topItem.Type != type)
        {
          isColorMatch = false;
        }
        else
        {
          isColorMatch = !topItem.IsUnknown;
        }
      }

      return isColorMatch && _items.Count < _height;
    }
  }
}