using System.Collections.Generic;
using CompanyName.Game.Colorsort;
using CompanyName.Level;

namespace CompanyName.LevelBuilderService
{
  public class ColorsortGameLevelData : GameLevelDataBase, IGoalsLevelData
  {
    private List<GoalsData> _goals;
    private List<ColorsortColumn> _columns;

    public ColorsortGameLevelData(List<GoalsData> goals, List<ColorsortColumn> columns, GameData gameData) : base(gameData)
    {
      _goals = goals;
      _columns = columns;
    }

    public List<GoalsData> Goals => _goals;
    public List<ColorsortColumn> Columns => _columns;
  }
}