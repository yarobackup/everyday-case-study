using System.Collections.Generic;
using CompanyName.Level;
using CompanyName.LevelBuilderService;
using CompanyName.Services.SL;
using CompanyName.WindowManager;
using PrimeTween;

namespace CompanyName.Ui
{
  public class GameScreenAnimatedPresenter : GameScreenPresenter
  {
    protected List<GoalsData> _levelGoals;
    public GameScreenAnimatedPresenter(GameSceneContext context) : base(context)
    {
    }
    public override void Init()
    {
      base.Init();
      Context.GetService(out IGoalsLevelData goalsData);
      _levelGoals = goalsData.Goals;
    }
    public override void OnStart()
    {
      Context.GetService(out GoalsPreviewPopUpData optionsData);
      var options = new GoalsPreviewPopUpOptions()
      {
        Data = optionsData,
        Goals = _levelGoals
      };

      var callbacks = new WindowCallbacks()
      {
        OnEnd = () =>
        {
          Tween.Delay(target: this, duration: 2f, onComplete: () =>
                  {
                    var callbacks = new WindowCallbacks()
                    {
                      OnEnd = () =>
                              {
                                base.OnStart();
                                _windowManager.OpenWindow(Screens.Game, Context);
                              }
                    };
                    _windowManager.CloseTopWindow(callbacks: callbacks);
                  });
        }
      };
      _windowManager.OpenPopUp(options, new PopUpOpenOptions(Screens.PopUp_GoalsPreview, true), callbacks);
    }
  }
}