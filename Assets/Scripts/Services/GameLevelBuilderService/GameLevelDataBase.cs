using CompanyName.Level;
using CompanyName.ProgressionService;

namespace CompanyName.LevelBuilderService
{
  public abstract class GameLevelDataBase : IGameLevelData
  {
    private GameData _gameData;

    public int Index => _gameData.Index;
    public int Streak => _gameData.Streak;
    public LevelDifficulty Difficulty => _gameData.Difficulty;
    public GameType GameType => _gameData.GameType;

    public GameLevelDataBase(GameData gameData)
    {
      _gameData = gameData;
    }
  }
}