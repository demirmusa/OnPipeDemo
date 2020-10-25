using UnityEngine;

namespace Utilities.PlayerInfoStore
{
    public class CollectedItemBucketCountStore
    {
        private const string Key = "COLLECTEDITEMBUCKETPERCENT";

        public static int Get() =>
            PlayerPrefs.GetInt(Key);


        public static void Set(int val) =>
            PlayerPrefs.SetInt(Key, val);
    }
}