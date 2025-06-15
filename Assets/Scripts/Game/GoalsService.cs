using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.Game.Colorsort;
using CompanyName.Level;
using CompanyName.LevelBuilderService;
using CompanyName.Services.SL;
using UnityEngine;

namespace CompanyName.Game
{
  public interface IGoalsService
  {
    bool IsAllGoalsCompleted { get; }
    void OnGoalChanged();
  }

  public class ColorsortGoalsService : IGoalsService, IInitializableWithContextService
  {
    private List<GoalsData> _goalsList;
    private ColorsortGameService _gameService;

    public bool IsAllGoalsCompleted { get; private set; }

    public void Init(MonoBehaviour context)
    {
      context.GetService(out IGoalsLevelData levelData).GetService(out _gameService);
      _goalsList = levelData.Goals;
    }

    public void OnGoalChanged()
    {
      var goal = _goalsList[0].amount;
      var solvedCount = 0;
      for (int i = 0; i < _gameService.Columns.Count; i++)
      {
        if (_gameService.Columns[i].IsSolved)
        {
          solvedCount++;
        }
        if (solvedCount >= goal)
        {
          IsAllGoalsCompleted = true;
          break;
        }
      }
    }
  }

  public enum GoalType
  {
    Any,
    Blue,
    Red,
    Yellow
  }
}