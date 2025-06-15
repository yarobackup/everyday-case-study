using CompanyName.WindowManager;

namespace CompanyName.Ui
{
  public class NoScreen<TPresenter> : ScreenBase<TPresenter> where TPresenter : class, IPresenter
  {
    public override string WindowID => string.Empty;
  }
}
