namespace CompanyName.Ui
{
  public interface IPresenter
  {
    void Init();
    void OnStart();
  }

  public abstract class PresenterBase<TContext>
  {
    protected TContext Context;
    public PresenterBase(TContext context)
    {
      Context = context;
    }
  }
}