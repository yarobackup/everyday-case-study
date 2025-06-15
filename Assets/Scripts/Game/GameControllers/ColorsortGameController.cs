
using CompanyName.Game.Colorsort;
using CompanyName.Services.SL;
using UnityEngine;

namespace CompanyName.Game.Woodoku
{
  public class ColorsortGameController : GameControllerBase
  {

    [SerializeField] private ColorsortColumnsController _columnsController;

    private ColumnSelectionService _selectionService;
    private IGoalsService _goalsService;
    private ColorsortGameService _gameService;

    protected override void OnAwake()
    {
      this.GetService(out _gameService)
      .GetService(out _selectionService)
      .GetService(out _goalsService);
    }

    protected override void OnStart()
    {
      _columnsController.Init(_gameService.Columns, OnColumnClick);
    }

    private void OnColumnClick(int index)
    {
      if (_selectionService.IsColumnSelected)
      {
        var selected = _selectionService.SelectedColumn;
        var isSameColumn = selected == index;
        if (isSameColumn)
        {
          _selectionService.SelectColumn(-1);
          _columnsController.Unselect(index);
        }
        else
        {
          var acceptCount = _gameService.PutToColumn(index, _gameService.GetItemType(selected));
          if (acceptCount == 0)
          {
            var peekCount = _gameService.PeekFromColumn(index);
            if (peekCount > 0)
            {
              _columnsController.Unselect(selected);
              _selectionService.SelectColumn(index);
              _columnsController.Select(index, peekCount);
            }
            return;
          }
          _columnsController.Unselect(selected);
          var movedCount = _gameService.MoveToColumn(selected, index);
          var isSolved = _gameService.Columns[index].IsSolved;
          _columnsController.MoveToColumn(selected, index, movedCount, isSolved);
          if (isSolved)
          {
            _goalsService.OnGoalChanged();
            if (_goalsService.IsAllGoalsCompleted)
            {
              ProceedGameWin();
            }
          }

          _selectionService.SelectColumn(-1);
        }
      }
      else
      {
        var peekCount = _gameService.PeekFromColumn(index);
        if (peekCount > 0)
        {
          _selectionService.SelectColumn(index);
          _columnsController.Select(index, peekCount);
        }
      }
    }

    protected override void OnGameIsReady()
    {
      _columnsController.Subscribe();
    }

    protected override void OnDispose()
    {
      base.OnDispose();
      _columnsController.Unsubscribe();
    }
  }
}