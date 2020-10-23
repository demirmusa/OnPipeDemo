using UnityEngine;

namespace Utilities.PlayerInfoStore
{
    public class PlayerCollectedItemStore
    {
        private const string CollectedItemCountKey = "COLLECTEDITEMCOUNT";
        private const string CollectedItemCountHighScoreKey = "COLLECTEDITEMCOUNTHIGHSCORE";

        public static int GetCurrent() =>
            PlayerPrefs.GetInt(CollectedItemCountKey);


        public static void StoreCurrent(int count)
        {
            var highScore = GetHighScore();
            if (count > highScore)
            {
                StoreHighScore(count);
            }

            PlayerPrefs.SetInt(CollectedItemCountKey, count);
        }

        public static int GetHighScore() =>
            PlayerPrefs.GetInt(CollectedItemCountHighScoreKey);

        private static void StoreHighScore(int count) =>
            PlayerPrefs.SetInt(CollectedItemCountHighScoreKey, count);
    }
}