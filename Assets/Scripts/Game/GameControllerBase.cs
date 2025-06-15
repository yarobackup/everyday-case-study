
using CompanyName.Services.SL;
using CompanyName.Ui;
using UnityEngine;

namespace CompanyName.Game
{
  public abstract class GameControllerBase : MonoBehaviour
  {
    [SerializeField] private UiButton _finishButton;

    protected IGameScreenPresenter _presenter;
    protected GameState _gameState;

    private void Awake()
    {
      this.GetService(out _presenter);
      _gameState = GameState.Preparing;
      _finishButton.SetInteractable(false);
      OnAwake();
    }

    private void Start()
    {
      OnStart();
    }

    private void OnDestroy()
    {
      OnDispose();
    }

    internal void SetGameIsReady()
    {
      _gameState = GameState.Ready;
      _finishButton.SetInteractable(true);
      _finishButton.Init("Finish");
      _finishButton.Subscibe(ProceedGameWin);
      OnGameIsReady();
    }

    protected abstract void OnAwake();
    protected abstract void OnGameIsReady();
    protected abstract void OnStart();
    protected virtual void OnDispose()
    {
      _finishButton.Unsubscibe();
    }

    protected void ProceedGameOver()
    {
      _presenter.ProceedGameOver();
    }

    protected void ProceedGameWin()
    {
      _presenter.ProceedGameWin();
      _presenter.ShowWinPopUp();
    }
  }
}