using System.Collections.Generic;
using CompanyName.LevelBuilderService;
using CompanyName.LevelLoaderService;
using CompanyName.Services.SL;

namespace CompanyName.Game
{
  public abstract class GameServicesInstallerBase : SceneMonoServiceInstaller<GameControllerBase>
  {
    private List<IInitializableWithContextService> _services = new List<IInitializableWithContextService>();

    public override void Register()
    {
      var levelData = CreateLevelData();
      this.RegisterAsSceneService(levelData);
      OnLevelDataRegistered(levelData);
      CreateGameServices();
      base.Register();
    }

    protected virtual void CreateGameServices()
    {
      this.RegisterAsSceneService(CreateGoalsService());
    }

    protected abstract IGoalsService CreateGoalsService();

    protected virtual void OnLevelDataRegistered(IGameLevelData levelData)
    {
      this.RegisterAsSceneService(levelData as IGoalsLevelData);
    }

    protected void AddToInitList(IInitializableWithContextService service)
    {
      _services.Add(service);
    }

    private IGameLevelData CreateLevelData()
    {
      ServiceLocator.Global.Get(out ILevelLoader levelLoader);
      if (levelLoader.CachedLevelData != null)
      {
        return levelLoader.CachedLevelData;
      }
      return new GameLevelBuilder().Build(levelLoader.GameToPlay);
    }

    public override void Init()
    {
      base.Init();
      for (int i = 0; i < _services.Count; i++)
      {
        _services[i].Init(this);
      }
    }
  }
}