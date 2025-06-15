using System.Collections.Generic;
using CompanyName.Level;
using CompanyName.Services.SL;
using CompanyName.WindowManager;
using TMPro;
using UnityEngine;

namespace CompanyName.Ui
{
  public class GoalsPreviewPopUpData
  {
    public bool ShowSimpleDesription { get; }
    public string DescriptionText { get; }

    public GoalsPreviewPopUpData(bool showSimpleDesription, string descriptionText = null)
    {
      ShowSimpleDesription = showSimpleDesription;
      DescriptionText = descriptionText;
    }
  }
  public class GoalsPreviewPopUpOptions : IPopUpOptions
  {
    public List<GoalsData> Goals;
    public GoalsPreviewPopUpData Data;
  }

  public class GoalsPreviewPopUp : UIWindow
  {
    [SerializeField] private float _closeDelay = 1f;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _goalsText;
    [SerializeField] private float _offsetOx = 8f;
    [SerializeField] private float _sizeOx = 40f;
    [SerializeField] private GoalPreviewItem _prefab;
    [SerializeField] private Transform _goalsParent;
    [SerializeField] private GoalsUiDataScriptable _goalsUiData;

    public override string WindowID => Screens.PopUp_GoalsPreview;

    private GoalsPreviewPopUpOptions _options;

    protected override void OnShowStart()
    {
      base.OnShowStart();
      Context.GetLocalService(out _options);
      ConfigureView(_options);
    }

    private void ConfigureView(GoalsPreviewPopUpOptions options)
    {
      _goalsText.SetText(options.Goals.Count == 1 ? "Goal" : "Goals");
      if (options.Data.ShowSimpleDesription)
      {
        _descriptionText.SetText(options.Data.DescriptionText);
      }
      else
      {
        CreateGoals(options.Goals);
      }
    }

    private void CreateGoals(List<GoalsData> goals)
    {
      var distance = _offsetOx + _sizeOx;
      var totalOffset = (goals.Count - 1) * distance;
      var startPosition = -totalOffset / 2;
      for (int i = 0; i < goals.Count; i++)
      {
        var item = Instantiate(_prefab, _goalsParent);
        var goal = goals[i];
        item.SetData(_goalsUiData.GetSprite(goal.goalType), goal.amount);
        item.Rt.anchoredPosition = new Vector2(startPosition + i * distance, 0);
      }
    }

    protected override void OnWindowHideCompleted()
    {
      base.OnWindowHideCompleted();
      _options.OnClosed?.Invoke();
      _options = null;
    }
  }
}
