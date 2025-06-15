using System;
using System.Collections.Generic;
using CompanyName.ProgressionService;
using UnityEngine;

namespace CompanyName.Ui
{
  public class DailyLayout : MonoBehaviour
  {
    [SerializeField] private RectTransform _levelItemsContainer;
    [SerializeField] private DailyLevelItem _levelItemPrefab;
    [SerializeField] private Vector2[] _positions;
    private List<DailyLevelItem> _levelItems;

    public void ConfigureScreen(CurrentProgress dailyProgress, Action<int> onLevelClick)
    {
      var levelsCount = dailyProgress.Levels.Count;
      EnsureSize(levelsCount);
      var acc = Vector2.zero;
      for (int i = 0; i < levelsCount; i++)
      {
        var leveData = dailyProgress.Levels[i];
        _levelItems[i].Configure(i, leveData, onLevelClick);
        acc += _positions[i > _positions.Length ? _positions.Length - 1 : i];
        (_levelItems[i].transform as RectTransform).anchoredPosition = acc;
      }
    }

    public void SubscribeLevels(bool isSubscribe)
    {
      for (int i = 0; i < _levelItems.Count; i++)
      {
        if (isSubscribe)
        {
          _levelItems[i].Subscribe();
        }
        else
        {
          _levelItems[i].Unsubscribe();
        }
      }
    }

    private void EnsureSize(int levelsCount)
    {
      if (_levelItems == null)
      {
        _levelItems = new List<DailyLevelItem>(levelsCount);
      }

      while (_levelItems.Count < levelsCount)
      {
        _levelItems.Add(Instantiate(_levelItemPrefab, _levelItemsContainer));
      }
      for (int i = 0; i < _levelItems.Count; i++)
      {
        _levelItems[i].gameObject.SetActive(i < levelsCount);
      }
    }
  }
}