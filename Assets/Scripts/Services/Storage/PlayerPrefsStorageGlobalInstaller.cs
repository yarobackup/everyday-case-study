using CompanyName.Services.SL;

namespace CompanyName.StorageService
{
  public class PlayerPrefsStorageGlobalInstaller : GlobalServiceInstaller<IStorage, PlayerPrefsStorage>
  {
    protected override PlayerPrefsStorage CreateService()
    {
      return new PlayerPrefsStorage();
    }
  }
}