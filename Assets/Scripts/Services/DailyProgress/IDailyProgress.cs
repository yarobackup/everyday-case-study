using CompanyName.ProgressionService;

namespace CompanyName.DailyProgressService
{
  public interface IDailyProgress
  {
    int CurrentStreak { get; set; }
    string CurrentDate { get; set; }

    void SetProgress(string today, CurrentProgress dailyData);
    bool TryGetProgress(string today, out CurrentProgress dailyData);
  }
}
