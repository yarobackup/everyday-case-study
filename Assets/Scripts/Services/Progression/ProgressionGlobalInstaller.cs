using CompanyName.LevelLoaderService;
using CompanyName.Services.SL;
using UnityEngine;

namespace CompanyName.ProgressionService
{
  public class ProgressionGlobalInstaller : GlobalInitializableServiceInstaller<IProgression, Progression>
  {
    [SerializeField] private StreaksDataScriptable _progression;
    protected override Progression CreateService()
    {
      return new Progression(_progression);
    }
  }
}