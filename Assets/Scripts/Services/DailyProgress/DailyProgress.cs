using CompanyName.ProgressionService;
using CompanyName.Services.SL;
using CompanyName.StorageService;

namespace CompanyName.DailyProgressService
{
  public class DailyProgress : IDailyProgress, IInitializableService
  {
    private const string CurrentStreakKey = "DailyProgress_CurrentStreak";
    private const string CurrentDateKey = "DailyProgress_CurrentDate";
    private const string DailyProgressProgressKey = "DailyProgress_Date_";
    public int CurrentStreak
    {
      get => _storage.Get(CurrentStreakKey, 0);
      set => _storage.Set(CurrentStreakKey, value);
    }
    public string CurrentDate
    {
      get => _storage.Get(CurrentDateKey, string.Empty);
      set => _storage.Set(CurrentDateKey, value ?? string.Empty);
    }

    private IStorage _storage;

    public void Init()
    {
      ServiceLocator.Global.GetService(out _storage);
    }

    public void SetProgress(string today, CurrentProgress dailyData)
    {
      var key = DailyProgressProgressKey + today;
      _storage.Set(key, dailyData);
    }

    public bool TryGetProgress(string today, out CurrentProgress dailyData)
    {
      var key = DailyProgressProgressKey + today;
      if (_storage.HasKey(key))
      {
        dailyData = _storage.Get<CurrentProgress>(key);
        return true;
      }
      dailyData = null;
      return false;
    }
  }
}
