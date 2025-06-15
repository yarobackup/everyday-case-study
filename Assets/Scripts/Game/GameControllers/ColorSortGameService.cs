using System.Collections.Generic;
using CompanyName.LevelBuilderService;
using CompanyName.Services.SL;
using UnityEngine;

namespace CompanyName.Game.Colorsort
{
  public class ColorsortGameService : IInitializableWithContextService
  {
    private List<ColorsortColumn> _columns;
    public List<ColorsortColumn> Columns => _columns;

    public void Init(MonoBehaviour context)
    {
      context.GetService(out ColorsortGameLevelData levelData);
      _columns = new List<ColorsortColumn>(levelData.Columns);
    }

    public int MoveToColumn(int from, int to)
    {
      var peekFrom = PeekFromColumn(from);
      if (peekFrom == 0)
      {
        return 0;
      }
      var putTo = PutToColumn(to, _columns[from].TopItem.Type);
      if (putTo == 0)
      {
        return 0;
      }
      var moveCount = Mathf.Min(peekFrom, putTo);
      for (int i = 0; i < moveCount; i++)
      {
        var topElem = _columns[from].RemoveFromTop();
        if (topElem != null)
        {
          if (!_columns[to].AddToTop(topElem))
          {
            Debug.LogError("Failed to add to column");
          }
        }
      }
      return moveCount;
    }

    public int PutToColumn(int index, ColorsortItemType type)
    {
      if (index < 0 || index >= _columns.Count)
      {
        return 0;
      }
      if (!_columns[index].CanPutToColumn(type))
      {
        return 0;
      }
      return _columns[index].FreeSpace;
    }

    public int PeekFromColumn(int index)
    {
      if (index < 0 || index >= _columns.Count)
      {
        return 0;
      }

      return _columns[index].PeekFromColumn();
    }

    public ColorsortItemType GetItemType(int index)
    {
      if (index < 0 || index >= _columns.Count)
      {
        throw new System.Exception();
      }
      return _columns[index].TopItem.Type;
    }
  }
}