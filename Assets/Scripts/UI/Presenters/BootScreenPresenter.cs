using CompanyName.Services.SL;
using CompanyNameLoaderService;

namespace CompanyName.Ui
{
  public interface IBootScreenPresenter : IPresenter
  {
    float MinWaitTime { get; }
  }

  public class BootScreenPresenter : PresenterBase<BootSceneContext>, IBootScreenPresenter
  {
    private BootScreenUiDataScriptable _uiData;
    private SceneLoader _sceneLoader;
    public float MinWaitTime => _uiData.MinWaitTime;
    public BootScreenPresenter(BootSceneContext context) : base(context)
    {
    }
    public void Init()
    {
      Context
      .GetService(out _uiData)
      .GetService(out _sceneLoader);
    }

    public void OnStart()
    {
      _sceneLoader.LoadSceneAsync(SceneNames.Main, _uiData.MinWaitTime);
    }
  }
}