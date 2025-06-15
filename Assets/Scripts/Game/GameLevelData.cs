using System;
using CompanyName.Game;
using CompanyName.ProgressionService;

namespace CompanyName.Level
{
    [Serializable]
    public class DailyData
    {
        public GameType GameType;
        public LevelDifficulty Difficulty;
    }

    [Serializable]
    public class GoalsData
    {
        public GoalType goalType;
        public int amount;
    }


    public class GameData
    {
        public int Index;
        public int Streak;
        public GameType GameType;
        public LevelDifficulty Difficulty;
    }
}
