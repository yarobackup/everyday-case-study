using CompanyName.Ui;
using CompanyName.Services.SL;
using UnityEngine;

namespace CompanyName
{
    public class BootSceneContext : BaseSceneContext<BootScreen, IBootScreenPresenter>
    {
        [SerializeField] private BootScreenUiDataScriptable _uiData;
        protected override string DefaultUiWindowId => Screens.Boot;
        protected override IBootScreenPresenter CreatePresenter() => new BootScreenPresenter(this);

        protected override void AwakeAdHoc()
        {
            this.RegisterAsSceneService(_uiData);
            base.AwakeAdHoc();
        }
    }
}
