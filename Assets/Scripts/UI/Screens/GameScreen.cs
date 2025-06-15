using UnityEngine;
using UnityEngine.UI;

namespace CompanyName.Ui
{
  public class GameScreen : ScreenBase<IGameScreenPresenter>
  {
    [SerializeField]
    private Button _buttonBack;

    public override string WindowID => Screens.Game;

    public override void Subscribe()
    {
      base.Subscribe();
      _buttonBack.onClick.AddListener(OnButtonClick);
    }

    public override void Unsubscribe()
    {
      base.Unsubscribe();
      _buttonBack.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
      _presenter.LoadMainScreen();
    }
  }
}
