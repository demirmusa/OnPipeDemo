using Collectables;
using UnityEngine;

namespace Utilities.PlayerInfoStore
{
    public class PlayerCollectableTypeInfoStore
    {
        private const string Key = "PLAYERSELECTEDCOLLECTABLETYPE";

        public static EnumCollectableType Get() =>
            (EnumCollectableType) PlayerPrefs.GetInt(Key);


        public static void Set(EnumCollectableType collectableType) =>
            PlayerPrefs.SetInt(Key, (int)collectableType);
    }
}