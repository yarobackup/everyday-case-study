using CompanyName.Ui;

namespace CompanyName
{
    public class MainSceneContext : BaseSceneContext<MainScreen, IMainScreenPresenter>
    {
        protected override string DefaultUiWindowId => Screens.Main;

        protected override IMainScreenPresenter CreatePresenter() => new MainScreenPresenter(this);
    }
}
