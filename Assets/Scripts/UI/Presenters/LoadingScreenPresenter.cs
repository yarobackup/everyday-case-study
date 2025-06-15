using System;
using CompanyName.Services.SL;
using CompanyName.TaskLoaderService;

namespace CompanyName.Ui
{
  public interface ILoadingScreenPresenter : IPresenter
  {
    event Action<float> OnProgressChanged;
    float Progress { get; }
  }
  public class LoadingScreenPresenter : PresenterBase<LoadingSceneContext>, ILoadingScreenPresenter
  {
    public event Action<float> OnProgressChanged;
    public float Progress { get; private set; }

    private ITaskLoader _taskLoader;

    public LoadingScreenPresenter(LoadingSceneContext context) : base(context)
    {
    }

    public void Init()
    {
      Context.GetService(out _taskLoader);

      SetProgress(0f);
      _taskLoader.OnProgressChanged += SetProgress;
      _taskLoader.OnLoadingCompleted += OnLoadingCompleted;
    }

    public void OnStart()
    {
      _taskLoader.StartLoading();
    }

    private void SetProgress(float progress)
    {
      Progress = progress;
      OnProgressChanged?.Invoke(Progress);
    }

    private void OnLoadingCompleted()
    {
      _taskLoader.OnProgressChanged -= SetProgress;
      _taskLoader.OnLoadingCompleted -= OnLoadingCompleted;
    }
  }
}