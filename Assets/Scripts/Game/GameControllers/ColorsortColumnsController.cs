using System;
using System.Collections.Generic;
using UnityEngine;

namespace CompanyName.Game.Colorsort
{
  public class ColorsortColumnsController : MonoBehaviour
  {
    [SerializeField] private ColorsortColumnItem _columnPrefab;
    [SerializeField] private RectTransform _columnsContainer;
    [SerializeField] private float _spacingX = 20;
    [SerializeField] private float _spacingY = 150;
    [SerializeField] private int _columnsPerRow = 3;
    private List<ColorsortColumnItem> _columns;

    internal void Init(List<ColorsortColumn> columns, Action<int> onColumnClick)
    {
      _columns = new List<ColorsortColumnItem>();
      for (int i = 0; i < columns.Count; i++)
      {
        var columnItem = Instantiate(_columnPrefab, _columnsContainer);
        columnItem.Init(i, columns[i].Height, columns[i].Items, onColumnClick);
        _columns.Add(columnItem);
      }
      CalculatePositions();
    }

    internal void Select(int index, int peekCount)
    {
      _columns[index].Select(peekCount);
    }

    internal void Unselect(int index)
    {
      _columns[index].Unselect();
    }

    internal void Subscribe()
    {
      for (int i = 0; i < _columns.Count; i++)
      {
        _columns[i].Subscribe();
      }
    }

    internal void Unsubscribe()
    {
      for (int i = 0; i < _columns.Count; i++)
      {
        _columns[i].Unsubscribe();
      }
    }
    private void CalculatePositions()
    {
      if (_columns.Count == 0)
      {
        return;
      }

      float columnWidth = _columns[0].Rt.rect.width;
      int totalRows = Mathf.CeilToInt((float)_columns.Count / _columnsPerRow);
      
      // Calculate height for each row
      float[] rowHeights = new float[totalRows];
      for (int row = 0; row < totalRows; row++)
      {
        float maxHeightInRow = 0f;
        int startIndex = row * _columnsPerRow;
        int endIndex = Mathf.Min(startIndex + _columnsPerRow, _columns.Count);
        
        for (int i = startIndex; i < endIndex; i++)
        {
          maxHeightInRow = Mathf.Max(maxHeightInRow, _columns[i].Rt.rect.height);
        }
        rowHeights[row] = maxHeightInRow;
      }
      
      // Calculate Y positions for each row
      float[] rowYPositions = new float[totalRows];
      float totalHeight = 0f;
      for (int i = 0; i < totalRows; i++)
      {
        totalHeight += rowHeights[i];
        if (i < totalRows - 1) totalHeight += _spacingY;
      }
      
      float currentY = totalHeight / 2f;
      for (int row = 0; row < totalRows; row++)
      {
        rowYPositions[row] = currentY - rowHeights[row] / 2f;
        currentY -= rowHeights[row] + _spacingY;
      }

      // Position columns
      for (int i = 0; i < _columns.Count; i++)
      {
        int row = i / _columnsPerRow;
        int col = i % _columnsPerRow;
        int columnsInThisRow = Mathf.Min(_columnsPerRow, _columns.Count - row * _columnsPerRow);
        
        // Calculate X position (centered for this row)
        float rowWidth = columnsInThisRow * columnWidth + (columnsInThisRow - 1) * _spacingX;
        float x = -rowWidth / 2f + columnWidth / 2f + col * (columnWidth + _spacingX);
        
        _columns[i].Rt.anchoredPosition = new Vector2(x, rowYPositions[row]);
      }
    }

    internal void MoveToColumn(int selected, int index, int movedCount, bool isSolved)
    {
      var blocksToMove = _columns[selected].RemoveFromTop(movedCount);
      _columns[index].AddToTop(blocksToMove, isSolved);
    }
  }
}