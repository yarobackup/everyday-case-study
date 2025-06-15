using UnityEngine;
using UnityEngine.UI;
using CompanyName.ProgressionService;
using TMPro;

namespace CompanyName.Ui
{
  public class MainScreen : ScreenBase<IMainScreenPresenter>
  {
    [SerializeField] private TMP_Text _streakText;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private DailyLayout _daily;

    private CurrentProgress _dailyProgress;
    public override string WindowID => Screens.Main;
    protected override void OnShowStart()
    {
      base.OnShowStart();
      _streakText.SetText($"Daily Streak: {_presenter.CurrentStreak}");
      _dailyProgress = _presenter.GetCurrentProgress();
      ConfigureScreen();
    }

    private void ConfigureScreen()
    {
      _daily.ConfigureScreen(_dailyProgress, OnLevelClick);
    }

    public override void Subscribe()
    {
      base.Subscribe();
      _settingsButton.onClick.AddListener(OnSettingsButtonClick);
      _daily.SubscribeLevels(true);
    }

    public override void Unsubscribe()
    {
      base.Unsubscribe();
      _settingsButton.onClick.RemoveListener(OnSettingsButtonClick);
      _daily.SubscribeLevels(false);
    }
    private void OnLevelClick(int index)
    {
      _presenter.LoadGame(index);
    }
    private void OnSettingsButtonClick()
    {
      _presenter.OpenSettings();
    }
  }
}
