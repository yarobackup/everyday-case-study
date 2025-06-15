
using System;
using CompanyName.PlayerSettingsService;
using CompanyName.Services.SL;
using UnityEngine;

namespace CompanyName.Ui
{
    public class ConfigurablePopUpView_Settings : ConfigurablePopUpView
    {
        [SerializeField] private UiCheckmark _vibrations;
        [SerializeField] private UiCheckmark _sounds;
        [SerializeField] private UiButton _contactUs;
        [SerializeField] private UiButton _privacyPolicy;
        [SerializeField] private UiButton _rateUs;

        private SettingsPopUpViewOptions _options;
        private IPlayerSettings _playerSettings;

        public override ConfigurablePopUpType ViewType => ConfigurablePopUpType.Settings;

        internal override void ConfigureView(IPopUpViewOptions viewOptions)
        {
            var options = viewOptions as SettingsPopUpViewOptions;
            if (options == null)
            {
                Debug.LogError("Invalid view options");
                return;
            }
            _options = options;
            ServiceLocator.Global.GetService(out _playerSettings);

            _vibrations.Init("Vibrations", !_playerSettings.IsVibrationsDisabled);
            _sounds.Init("Sounds", !_playerSettings.IsSoundsDisabled);
            _contactUs.Init("Contact Us");
            _privacyPolicy.Init("Privacy Policy");
            _rateUs.Init("Rate Us");
        }

        internal override void Subscribe()
        {
            base.Subscribe();
            _vibrations.Subscibe(OnVibrationsToggle);
            _sounds.Subscibe(OnSoundsToggle);
            _contactUs.Subscibe(() =>
            {
                string email = _options.Email;
                string subject = "Contact Us Request";
                string body = "Dear team,\n\nI need help with the following:\n\n";

                string mailtoLink = $"mailto:{email}?subject={Uri.EscapeDataString(subject)}&body={Uri.EscapeDataString(body)}";
                Application.OpenURL(mailtoLink);
            });
            _privacyPolicy.Subscibe(() =>
            {
                Application.OpenURL(_options.PrivacyPolicyUrl);
            });
            _rateUs.Subscibe(() =>
            {
                Application.OpenURL(_options.RateUsUrl);
            });
        }

        private void OnSoundsToggle(bool isOn)
        {
            _playerSettings.SetSoundsDisabled(!isOn);
        }

        private void OnVibrationsToggle(bool isOn)
        {
            _playerSettings.SetVibrationsDisabled(!isOn);
        }

        internal override void Unsubscribe()
        {
            base.Unsubscribe();
            _vibrations.Unsubscibe();
            _sounds.Unsubscibe();
            _contactUs.Unsubscibe();
            _privacyPolicy.Unsubscibe();
            _rateUs.Unsubscibe();
        }

        internal override void Reset()
        {
            base.Reset();
            _options = null;

        }
    }
}
