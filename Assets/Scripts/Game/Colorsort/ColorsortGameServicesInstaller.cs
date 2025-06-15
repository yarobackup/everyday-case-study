
using CompanyName.LevelBuilderService;
using CompanyName.Services.SL;
using CompanyName.Ui;

namespace CompanyName.Game.Colorsort
{
  public class ColorsortGameServicesInstaller : GameServicesInstallerBase
  {
    protected override void CreateGameServices()
    {
      base.CreateGameServices();

      this.RegisterAsSceneService(new ColumnSelectionService());

      var gameService = new ColorsortGameService();
      this.RegisterAsSceneService(gameService);
      AddToInitList(gameService);

      this.RegisterAsSceneService(new GoalsPreviewPopUpData(true, "Sort all colors to win"));
    }

    protected override IGoalsService CreateGoalsService()
    {
      var service = new ColorsortGoalsService();
      AddToInitList(service);
      return service;
    }

    protected override void OnLevelDataRegistered(IGameLevelData levelData)
    {
      base.OnLevelDataRegistered(levelData);
      this.RegisterAsSceneService(levelData as ColorsortGameLevelData);
    }
  }
}