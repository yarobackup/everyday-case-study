using UnityEngine;

namespace CompanyName.PlayerSettingsService
{
    public class PlayerSettings : IPlayerSettings
    {
        private const string VibrationsKey = "PlayerSettings_VibrationsDisabled";
        private const string SoundsKey = "PlayerSettings_SoundsDisabled";

        public bool IsVibrationsDisabled => PlayerPrefs.GetInt(VibrationsKey, 0) == 1;
        public bool IsSoundsDisabled => PlayerPrefs.GetInt(SoundsKey, 0) == 1;
        public void SetVibrationsDisabled(bool isDisabled)
        {
            PlayerPrefs.SetInt(VibrationsKey, isDisabled ? 1 : 0);
        }
        public void SetSoundsDisabled(bool isDisabled)
        {
            PlayerPrefs.SetInt(SoundsKey, isDisabled ? 1 : 0);
        }
    }
}
