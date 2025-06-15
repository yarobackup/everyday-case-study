using CompanyName.Level;
using CompanyName.LevelBuilderService;
using UnityEngine;

namespace CompanyName.LevelLoaderService
{
    public class LevelLoader : MonoBehaviour, ILevelLoader
    {
        [SerializeField]
        private StreaksDataScriptable _streaksData;

        public IGameLevelData CachedLevelData { get; set; }

        public int UniqueLevelsCount => _streaksData == null ? 0 : _streaksData.Count;

        public GameData GameToPlay { get; set; }

        public DailyData LoadLevelData(GameData gameData)
        {
            if (_streaksData == null || !_streaksData.TryGelLevel(gameData.Streak, gameData.Index, out var data))
            {
                return null;
            }
            return data;
        }
    }
}
