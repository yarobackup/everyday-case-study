using System;
using System.Collections.Generic;
using CompanyName.LevelLoaderService;

namespace CompanyName.ProgressionService
{
  [Serializable]
  public class DailyLevelData
  {
    public LevelDifficulty Difficulty;
    public LevelState State;
    public GameType GameType;

    public bool Playable => State == LevelState.Active || State == LevelState.Failed;
  }

  [Serializable]
  public class CurrentProgress
  {
    public List<DailyLevelData> _levels;

    public bool IsAllLevelsCompleted
    {
      get
      {
        for (int i = 0; i < _levels.Count; i++)
        {
          if (_levels[i].State != LevelState.Completed)
          {
            return false;
          }
        }
        return true;
      }
    }

    public List<DailyLevelData> Levels => _levels;

    public CurrentProgress(LevelDataScriptable levelDataScriptable)
    {
      _levels = new List<DailyLevelData>();
      for (int i = 0; i < levelDataScriptable.dailyData.Length; i++)
      {
        _levels.Add(new DailyLevelData()
        {
          Difficulty = levelDataScriptable.dailyData[i].Difficulty,
          GameType = levelDataScriptable.dailyData[i].GameType,
          State = i == 0 ? LevelState.Active : LevelState.Locked
        });
      }
    }

    public bool UpdateLevelAtIndex(int index, LevelState state)
    {
      if (index < 0 || index > _levels.Count || _levels[index].State == state)
      {
        return false;
      }
      _levels[index].State = state;
      return true;
    }
  }
}