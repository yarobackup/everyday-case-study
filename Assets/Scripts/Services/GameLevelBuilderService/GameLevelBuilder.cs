using System.Collections.Generic;
using CompanyName.Level;
using CompanyName.ProgressionService;

namespace CompanyName.LevelBuilderService
{
  public interface IGameLevelData
  {
    int Index { get; }
    int Streak { get; }
    GameType GameType { get; }
    LevelDifficulty Difficulty { get; }
  }

  public interface IGoalsLevelData
  {
    List<GoalsData> Goals { get; }
  }
  public interface IGameLevelBuilder
  {
    IGameLevelData Build(GameData gameData);
  }
  public class GameLevelBuilder
  {
    private readonly Dictionary<GameType, IGameLevelBuilder> _builders;

    public GameLevelBuilder()
    {
      _builders = new Dictionary<GameType, IGameLevelBuilder>
      {
          { GameType.Colorsort, new ColorsortGameLevelBuilder() }
      };
    }

    public IGameLevelData Build(GameData gameData)
    {
      if (!_builders.TryGetValue(gameData.GameType, out var builder))
      {
        throw new System.Exception($"No builder found for game type: {gameData.GameType}");
      }
      return builder.Build(gameData);
    }
  }
}
