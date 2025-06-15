using System;
using CompanyName.DailyProgressService;
using CompanyName.DateService;
using CompanyName.LevelLoaderService;
using CompanyName.Services.SL;

namespace CompanyName.ProgressionService
{
  public class Progression : IProgression, IInitializableService
  {
    private readonly StreaksDataScriptable _dailyData;
    private IDateProvider _dateProvider;
    private IDailyProgress _dailyProgress;

    public int CurrentStreak => _dailyProgress.CurrentStreak;

    public Progression(StreaksDataScriptable dailyData)
    {
      _dailyData = dailyData;
    }

    public void Init()
    {
      ServiceLocator.Global.GetService(out _dateProvider).GetService(out _dailyProgress);
      // TODO Add logic to check streak when user is inside the app an day change or app moved from background
      CheckStreak();
    }

    private void CheckStreak()
    {
      var today = _dateProvider.Today;
      if (today == _dailyProgress.CurrentDate)
      {
        return;
      }
      if (_dailyProgress.CurrentDate == string.Empty)
      {
        _dailyProgress.CurrentStreak = 0;
        _dailyProgress.CurrentDate = today;
        return;
      }
      var todayAsDate = DateTime.Parse(today);
      var lastActivityAsDate = DateTime.Parse(_dailyProgress.CurrentDate);
      if (todayAsDate.Subtract(lastActivityAsDate).TotalDays > 1)
      {
        _dailyProgress.CurrentStreak = 0;
      }
      else
      {
        if (!_dailyProgress.TryGetProgress(_dailyProgress.CurrentDate, out var dailyData) || !dailyData.IsAllLevelsCompleted)
        {
          _dailyProgress.CurrentStreak = 0;
        }
      }
      _dailyProgress.CurrentDate = today;
    }

    public CurrentProgress GetCurrent()
    {
      if (_dailyProgress.TryGetProgress(_dailyProgress.CurrentDate, out var dailyData))
      {
        return dailyData;
      }
      return new CurrentProgress(_dailyData.GetLevelAsset(CurrentStreak));
    }

    public CurrentProgress UpdateCurrent(int index, LevelState state, LevelState? nextLevelState)
    {
      var currentProgress = GetCurrent();
      if (currentProgress.UpdateLevelAtIndex(index, state))
      {
        if (nextLevelState.HasValue && index + 1 < currentProgress.Levels.Count)
        {
          currentProgress.UpdateLevelAtIndex(index + 1, nextLevelState.Value);
        }
        _dailyProgress.SetProgress(_dailyProgress.CurrentDate, currentProgress);

        if (currentProgress.IsAllLevelsCompleted)
        {
          _dailyProgress.CurrentStreak++;
        }
      }
      return currentProgress;
    }
  }
}