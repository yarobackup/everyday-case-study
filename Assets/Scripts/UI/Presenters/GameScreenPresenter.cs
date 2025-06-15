using CompanyName.Game;
using CompanyName.LevelBuilderService;
using CompanyName.LevelLoaderService;
using CompanyName.ProgressionService;
using CompanyName.Services.SL;
using CompanyName.WindowManager;
using CompanyNameLoaderService;

namespace CompanyName.Ui
{
  public interface IGameScreenPresenter : IPresenter
  {
    void LoadMainScreen();
    void ProceedGameOver();
    void ProceedGameWin();
    void ShowWinPopUp();
  }
  public class GameScreenPresenter : PresenterBase<GameSceneContext>, IGameScreenPresenter
  {
    protected IWindowManager _windowManager;
    protected SceneLoader _sceneLoader;
    protected IProgression _progression;
    protected ILevelLoader _levelLoader;
    protected GameControllerBase _gameController;

    public GameScreenPresenter(GameSceneContext context) : base(context)
    {
    }
    public virtual void Init()
    {
      Context.GetService(out _sceneLoader)
      .GetService(out _windowManager)
      .GetService(out _progression)
      .GetService(out _gameController)
      .GetService(out _levelLoader);
    }
    public virtual void OnStart()
    {
      _gameController.SetGameIsReady();
    }

    public void LoadMainScreen()
    {
      _levelLoader.CachedLevelData = null;
      _sceneLoader.LoadScene(SceneNames.Main);
    }

    public void ProceedGameOver()
    {
      var viewOpt = new TextPopUpViewOptions("You lost this round");
      var cfg = new ConfigurablePopUpOptions("Game Over!", ConfigurablePopUpType.TwoLinesDescription, viewOpt)
      {
        PrimaryButtonOptions = new ButtonOptions()
        {
          Text = "Try Again",
          BtnHandler = ReloadCurrentLevel
        },
        CloseButtonOptions = new CloseButtonOptions()
        {
          Delay = 3f,
          BtnHandler = LoadMainScreen
        }
      };
      _windowManager.OpenPopUp(cfg, new PopUpOpenOptions(Screens.PopUp_Configurable, false));
    }

    public void ProceedGameWin()
    {
      Context.GetService(out IGameLevelData data);
      _progression.UpdateCurrent(data.Index, LevelState.Completed, LevelState.Active);
      var options = new WindowOptions()
      {
        AnimationType = WindowAnimationType.None
      };
      _windowManager.CloseTopWindow(options);
    }

    public void ShowWinPopUp()
    {
      var viewOpt = new TextPopUpViewOptions("You finished this level");
      var cfg = new ConfigurablePopUpOptions("Congratulations!", ConfigurablePopUpType.SingleLineDescription, viewOpt)
      {
        PrimaryButtonOptions = new ButtonOptions()
        {
          Text = "Main",
          BtnHandler = LoadMainScreen
        }
      };
      _windowManager.OpenPopUp(cfg, new PopUpOpenOptions(Screens.PopUp_Configurable, false));
    }

    private void ReloadCurrentLevel()
    {
      Context.GetService(out IGameLevelData data);
      _levelLoader.CachedLevelData = data;
      ReloadGameScene();
    }
    private void ReloadGameScene()
    {
      _windowManager.CloseTopWindow(callbacks: new WindowCallbacks()
      {
        OnEnd = () =>
        {
          _windowManager.CloseAllWindows();
          _sceneLoader.LoadScene(SceneNames.GameColorsort);
        }
      });
    }
  }
}