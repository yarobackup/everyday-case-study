using CompanyName.Services.SL;

namespace CompanyName.DailyProgressService
{
  public class DailyProgressGlobalInstaller : GlobalInitializableServiceInstaller<IDailyProgress, DailyProgress>
  {
    protected override DailyProgress CreateService()
    {
      return new DailyProgress();
    }
  }
}