using CompanyName.Level;
using CompanyName.LevelBuilderService;

namespace CompanyName.LevelLoaderService
{
    public interface ILevelLoader
    {
        DailyData LoadLevelData(GameData gameData);
        GameData GameToPlay { get; set; }
        IGameLevelData CachedLevelData { get; set; }
        int UniqueLevelsCount { get; }
    }
}
