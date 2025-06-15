using CompanyName.Ui;

namespace CompanyName
{
    public class GameSceneContext : BaseSceneContext<GameScreen, IGameScreenPresenter>
    {
        protected override string DefaultUiWindowId => Screens.Game;

        protected override IGameScreenPresenter CreatePresenter() => HasDefaultUiWindow ? new GameScreenPresenter(this) : new GameScreenAnimatedPresenter(this);
    }
}
