using CompanyName.Services.SL;
using CompanyName.WindowManager;

namespace CompanyName.Ui
{
  public abstract class ScreenBase<TPresenter> : UIWindow where TPresenter : class, IPresenter
  {
    protected TPresenter _presenter;

    protected override void OnSetContextAdHoc()
    {
      base.OnSetContextAdHoc();
      Context.GetService(out _presenter);
    }
  }
}