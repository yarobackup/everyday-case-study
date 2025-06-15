using CompanyName.Services.SL;

namespace CompanyName.PlayerSettingsService
{
  public class PlayerSettingsGlobalInstaller : GlobalServiceInstaller<IPlayerSettings, PlayerSettings>
  {
    protected override PlayerSettings CreateService()
    {
      return new PlayerSettings();
    }
  }
}