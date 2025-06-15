namespace CompanyName.PlayerSettingsService
{
    public interface IPlayerSettings
    {
        bool IsVibrationsDisabled { get; }
        bool IsSoundsDisabled { get; }
        void SetVibrationsDisabled(bool isDisabled);
        void SetSoundsDisabled(bool isDisabled);
    }
}
