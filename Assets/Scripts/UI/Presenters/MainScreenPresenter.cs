using CompanyName.Services.SL;
using CompanyName.TaskLoaderService;
using CompanyName.WindowManager;
using CompanyNameLoaderService;
using CompanyName.LevelLoaderService;
using CompanyName.ProgressionService;
using CompanyName.Level;
using System;

namespace CompanyName.Ui
{
  public interface IMainScreenPresenter : IPresenter
  {
    int CurrentStreak { get; }
    CurrentProgress GetCurrentProgress();
    void LoadGame(int index);
    void OpenSettings();
  }
  public class MainScreenPresenter : PresenterBase<MainSceneContext>, IMainScreenPresenter
  {
    private SceneLoader _sceneLoader;
    private IWindowManager _windowManager;
    private ITaskLoader _taskLoader;
    private ILevelLoader _levelLoader;

    private IProgression _progress;
    private CurrentProgress _dailyProgress;

    public int CurrentStreak => _progress.CurrentStreak;

    public MainScreenPresenter(MainSceneContext context) : base(context)
    {
    }
    public void Init()
    {
      Context
      .GetService(out _sceneLoader)
      .GetService(out _taskLoader)
      .GetService(out _levelLoader)
      .GetService(out _progress)
      .GetService(out _windowManager);
      Configure();
    }

    public void Configure()
    {
      _dailyProgress = _progress.GetCurrent();
    }

    public void OnStart() { }

    public void LoadGame(int index)
    {
      var level = _dailyProgress.Levels[index];
      if (!level.Playable)
      {
        return;
      }
      var gameData = new GameData()
      {
        Streak = _progress.CurrentStreak,
        GameType = level.GameType,
        Index = index,
        Difficulty = level.Difficulty
      };
      _levelLoader.GameToPlay = gameData;
      LoadGameScene(level.GameType);
    }

    private void LoadGameScene(GameType gameType)
    {
      switch (gameType)
      {
        case GameType.Colorsort:
          _sceneLoader.LoadScene(SceneNames.GameColorsort);
          break;
        default:
          throw new Exception("Game type not supported");
      }
    }

    public void OpenSettings()
    {
      var viewOpt = new SettingsPopUpViewOptions();
      var cfg = new ConfigurablePopUpOptions("Settings", ConfigurablePopUpType.Settings, viewOpt)
      {
        CloseButtonOptions = new CloseButtonOptions()
        {
          BtnHandler = () =>
          {
            _windowManager.CloseTopWindow();
          }
        }
      };
      _windowManager.OpenPopUp(cfg, new PopUpOpenOptions(Screens.PopUp_Configurable, false));
    }

    public CurrentProgress GetCurrentProgress()
    {
      return _dailyProgress;
    }
  }
}