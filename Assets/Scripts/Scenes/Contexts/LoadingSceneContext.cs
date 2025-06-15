using CompanyName.Ui;
using UnityEngine;
namespace CompanyName
{
    public class LoadingSceneContext : BaseSceneContext<NoScreen<ILoadingScreenPresenter>, ILoadingScreenPresenter>
    {
        [SerializeField]
        private LoadingScreen _loadingScreen;
        protected override string DefaultUiWindowId => null;

        protected override void StartAdHoc()
        {
            _loadingScreen.OnProgressChanged(_presenter.Progress);
            _loadingScreen.SetSliderValue(0f);
            _presenter.OnProgressChanged += _loadingScreen.OnProgressChanged;

            base.StartAdHoc();
            _presenter.OnStart();
        }

        protected override void DestroyAdHoc()
        {
            _presenter.OnProgressChanged -= _loadingScreen.OnProgressChanged;
            base.DestroyAdHoc();
        }

        protected override ILoadingScreenPresenter CreatePresenter() => new LoadingScreenPresenter(this);
    }
}
