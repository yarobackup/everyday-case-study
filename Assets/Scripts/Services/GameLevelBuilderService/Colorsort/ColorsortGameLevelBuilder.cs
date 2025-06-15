using System.Collections.Generic;
using CompanyName.Game;
using CompanyName.Game.Colorsort;
using CompanyName.Level;
using CompanyName.ProgressionService;

namespace CompanyName.LevelBuilderService
{
  public class ColorsortGameLevelBuilder : IGameLevelBuilder
  {

    public IGameLevelData Build(GameData gameData)
    {
      switch (gameData.Difficulty)
      {
        case LevelDifficulty.Easy:
          {
            var goals = new List<GoalsData>() { new GoalsData() { goalType = GoalType.Any, amount = 2 }, };
            var columns = new List<ColorsortColumn>(3);
            var col1 = new ColorsortColumn(4);
            col1.AddToTop(new ColorsortItem(ColorsortItemType.Type1, false));
            col1.AddToTop(new ColorsortItem(ColorsortItemType.Type1, false));
            col1.AddToTop(new ColorsortItem(ColorsortItemType.Type2, false));
            col1.AddToTop(new ColorsortItem(ColorsortItemType.Type1, false));
            columns.Add(col1);
            var col2 = new ColorsortColumn(4);
            col2.AddToTop(new ColorsortItem(ColorsortItemType.Type2, false));
            col2.AddToTop(new ColorsortItem(ColorsortItemType.Type2, false));
            col2.AddToTop(new ColorsortItem(ColorsortItemType.Type1, false));
            col2.AddToTop(new ColorsortItem(ColorsortItemType.Type2, false));
            columns.Add(col2);
            var col3 = new ColorsortColumn(4);
            columns.Add(col3);

            return new ColorsortGameLevelData(goals, columns, gameData);
          }
        case LevelDifficulty.Medium:
          {
            var goals = new List<GoalsData>() { new GoalsData() { goalType = GoalType.Any, amount = 2 }, };
            var columns = new List<ColorsortColumn>(3);
            var col1 = new ColorsortColumn(4);
            col1.AddToTop(new ColorsortItem(ColorsortItemType.Type1, false));
            col1.AddToTop(new ColorsortItem(ColorsortItemType.Type2, false));
            col1.AddToTop(new ColorsortItem(ColorsortItemType.Type1, false));
            col1.AddToTop(new ColorsortItem(ColorsortItemType.Type2, false));
            columns.Add(col1);
            var col2 = new ColorsortColumn(4);
            col2.AddToTop(new ColorsortItem(ColorsortItemType.Type2, false));
            col2.AddToTop(new ColorsortItem(ColorsortItemType.Type1, false));
            col2.AddToTop(new ColorsortItem(ColorsortItemType.Type2, false));
            col2.AddToTop(new ColorsortItem(ColorsortItemType.Type1, false));
            columns.Add(col2);
            var col3 = new ColorsortColumn(4);
            columns.Add(col3);

            return new ColorsortGameLevelData(goals, columns, gameData);
          }
        case LevelDifficulty.Hard:
          {
            {
              var goals = new List<GoalsData>() { new GoalsData() { goalType = GoalType.Any, amount = 3 }, };
              var columns = new List<ColorsortColumn>(5);
              var emptyCount = 2;
              var col1 = new ColorsortColumn(4);
              col1.AddToTop(new ColorsortItem(ColorsortItemType.Type1, false));
              col1.AddToTop(new ColorsortItem(ColorsortItemType.Type2, false));
              col1.AddToTop(new ColorsortItem(ColorsortItemType.Type3, false));
              col1.AddToTop(new ColorsortItem(ColorsortItemType.Type1, false));
              columns.Add(col1);
              var col2 = new ColorsortColumn(4);
              col2.AddToTop(new ColorsortItem(ColorsortItemType.Type2, false));
              col2.AddToTop(new ColorsortItem(ColorsortItemType.Type3, false));
              col2.AddToTop(new ColorsortItem(ColorsortItemType.Type1, false));
              col2.AddToTop(new ColorsortItem(ColorsortItemType.Type1, false));
              columns.Add(col2);
              var col3 = new ColorsortColumn(4);
              col3.AddToTop(new ColorsortItem(ColorsortItemType.Type3, false));
              col3.AddToTop(new ColorsortItem(ColorsortItemType.Type2, false));
              col3.AddToTop(new ColorsortItem(ColorsortItemType.Type3, false));
              col3.AddToTop(new ColorsortItem(ColorsortItemType.Type2, false));
              columns.Add(col3);

              for (int i = 0; i < emptyCount; i++)
              {
                var col = new ColorsortColumn(4);
                columns.Add(col);
              }

              return new ColorsortGameLevelData(goals, columns, gameData);
            }
          }
        case LevelDifficulty.Ultimate:
          {
            {
              var goals = new List<GoalsData>() { new GoalsData() { goalType = GoalType.Any, amount = 4 }, };
              var columns = new List<ColorsortColumn>(5);
              var emptyCount = 2;
              var col1 = new ColorsortColumn(4);
              col1.AddToTop(new ColorsortItem(ColorsortItemType.Type2, false));
              col1.AddToTop(new ColorsortItem(ColorsortItemType.Type3, false));
              col1.AddToTop(new ColorsortItem(ColorsortItemType.Type2, false));
              col1.AddToTop(new ColorsortItem(ColorsortItemType.Type3, false));
              columns.Add(col1);
              var col2 = new ColorsortColumn(4);
              col2.AddToTop(new ColorsortItem(ColorsortItemType.Type1, false));
              col2.AddToTop(new ColorsortItem(ColorsortItemType.Type1, false));
              col2.AddToTop(new ColorsortItem(ColorsortItemType.Type2, false));
              col2.AddToTop(new ColorsortItem(ColorsortItemType.Type3, false));
              columns.Add(col2);
              var col3 = new ColorsortColumn(4);
              col3.AddToTop(new ColorsortItem(ColorsortItemType.Type1, false));
              col3.AddToTop(new ColorsortItem(ColorsortItemType.Type4, false));
              col3.AddToTop(new ColorsortItem(ColorsortItemType.Type3, false));
              col3.AddToTop(new ColorsortItem(ColorsortItemType.Type4, false));
              columns.Add(col3);
              var col4 = new ColorsortColumn(4);
              col4.AddToTop(new ColorsortItem(ColorsortItemType.Type2, false));
              col4.AddToTop(new ColorsortItem(ColorsortItemType.Type1, false));
              col4.AddToTop(new ColorsortItem(ColorsortItemType.Type4, false));
              col4.AddToTop(new ColorsortItem(ColorsortItemType.Type4, false));
              columns.Add(col4);

              for (int i = 0; i < emptyCount; i++)
              {
                var col = new ColorsortColumn(4);
                columns.Add(col);
              }

              return new ColorsortGameLevelData(goals, columns, gameData);
            }
          }
      }
      return null;
    }
  }
}