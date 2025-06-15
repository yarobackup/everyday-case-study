using UnityEngine;
using UnityEngine.UI;

namespace CompanyName.Ui
{
    public class ConfigurablePopUpView_SingleSprite : ConfigurablePopUpView
    {
        [SerializeField]
        private Image _image;

        public override ConfigurablePopUpType ViewType => ConfigurablePopUpType.SingleSprite;

        internal override void ConfigureView(IPopUpViewOptions viewOptions)
        {
            var singleLineOptions = viewOptions as SpritePopUpViewOptions;
            if (singleLineOptions == null)
            {
                Debug.LogError("Invalid view options");
                return;
            
            }
            _image.sprite = singleLineOptions.Sprite;
        }
    }
}
