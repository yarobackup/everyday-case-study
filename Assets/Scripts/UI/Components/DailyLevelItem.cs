using System;
using CompanyName.ProgressionService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CompanyName.Ui
{
  public class DailyLevelItem : MonoBehaviour
  {
    [SerializeField] private TMP_Text _difficultyText;
    [SerializeField] private UiButton _button;
    [SerializeField] private GameObject _completed;
    [SerializeField] private GameObject _locked;
    private int _index;
    private Action<int> _onLevelClick;

    private DailyLevelData _levelData;

    public void Configure(int i, DailyLevelData leveData, Action<int> onLevelClick)
    {
      _index = i;
      _levelData = leveData;
      _onLevelClick = onLevelClick;
      var canPlay = _levelData.State == LevelState.Active || _levelData.State == LevelState.Failed;
      _button.Init(canPlay ? $"{i + 1}" : string.Empty);
      _button.SetInteractable(canPlay);
      _difficultyText.SetText(leveData.Difficulty.ToString());
      _completed.SetActive(_levelData.State == LevelState.Completed);
      _locked.SetActive(_levelData.State == LevelState.Locked);
    }

    internal void Subscribe()
    {
      _button.Subscibe(InvokeOnLevelClick);
    }

    internal void Unsubscribe()
    {
      _button.Unsubscibe();
    }
    private void InvokeOnLevelClick()
    {
      _onLevelClick?.Invoke(_index);
    }
  }
}