using UnityEngine;

namespace Utilities.PlayerInfoStore
{
    public class PlayerLevelStore
    {
        private const string LevelPrefKey = "PLAYERLEVEL";

        public static int GetCurrentLevel()
        {
            var level = PlayerPrefs.GetInt(LevelPrefKey);
            if (level != 0)
                return level;

            PlayerPrefs.SetInt(LevelPrefKey, 1);
            return 1;
        }

        public static void AddLevel()
        {
            var currentLevel = GetCurrentLevel();
            PlayerPrefs.SetInt(LevelPrefKey, currentLevel + 1);
        }
    }
}