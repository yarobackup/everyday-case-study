using TMPro;
using UnityEngine;

namespace CompanyName.Ui
{
    public class ConfigurablePopUpView_SingleLine : ConfigurablePopUpView
    {
        [SerializeField]
        private TMP_Text _descriptionText;

        public override ConfigurablePopUpType ViewType => ConfigurablePopUpType.SingleLineDescription;

        internal override void ConfigureView(IPopUpViewOptions viewOptions)
        {
            var singleLineOptions = viewOptions as TextPopUpViewOptions;
            if (singleLineOptions == null)
            {
                Debug.LogError("Invalid view options");
                return;
            }
            _descriptionText.SetText(singleLineOptions.Description);
        }
    }
}
