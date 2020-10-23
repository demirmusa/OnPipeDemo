using System.Linq;
using UnityEngine;
using Utilities;
using Utilities.PlayerInfoStore;
using Random = UnityEngine.Random;

namespace MapCreation
{
    public class LevelManager
    {
        private const string LastRequestedLevelKey = "LastRequestedLevel";
        private const string LastRequestedLevelListKey = "LastRequestedLevelList";

        private int _minTileLengthInALevel;
        private int _maxTileLengthInALevel;

        private Tile[] _allMapTiles;

        /// <summary>
        /// Tüm leveller rastgele oluşturuluyor. Eğer level bir kere oluşturulduysa o kaydediliyor ve geçilene kadar aynı level kullanıcıya sunuluyor.
        /// </summary>
        /// <param name="minTileLengthInALevel"></param>
        /// <param name="maxTileLengthInALevel"></param>
        /// <param name="allMapTiles"></param>
        public LevelManager(int minTileLengthInALevel, int maxTileLengthInALevel, Tile[] allMapTiles)
        {
            _allMapTiles = allMapTiles;
            _minTileLengthInALevel = minTileLengthInALevel;
            _maxTileLengthInALevel = maxTileLengthInALevel;
        }

        public Tile[] GetLevelTiles()
        {
            var level = PlayerLevelStore.GetCurrentLevel();

            var lastRequestedLevel = PlayerPrefs.GetInt(LastRequestedLevelKey);

#if DEBUG
            return NewRandomLevel(level);
#endif
            return lastRequestedLevel == level
                ? GetSavedLevel(level)
                : NewRandomLevel(level);
        }

        private Tile[] GetSavedLevel(int level)
        {
            var savedLevelsTileIds = PlayerPrefList.GetList<int>(LastRequestedLevelListKey);
            //son üretilen levelde bir hata varsa yeni üret
            if (savedLevelsTileIds == null || savedLevelsTileIds.Count == 0)
            {
                return NewRandomLevel(level);
            }

            //son üretilen level player prefte idler ile tutuluyor. Bu idlere ait tile listesini oluştur.
            var tiles = new Tile[savedLevelsTileIds.Count];

            for (var index = 0; index < savedLevelsTileIds.Count; index++)
            {
                tiles[index] = GetTileByIdOrDefault(savedLevelsTileIds[index], level);
            }

            return tiles;
        }

        private Tile[] NewRandomLevel(int level)
        {
            ClearCurrentLevelInfo();
            var availableTiles = _allMapTiles.Where(t => t.minRequiredPlayerLevel <= level).ToList();

            int tileCountInLevel = Random.Range(_minTileLengthInALevel, _maxTileLengthInALevel + 1);

            var tiles = new Tile[tileCountInLevel];
            for (int i = 0; i < tileCountInLevel; i++)
            {
                tiles[i] = availableTiles[Random.Range(0, availableTiles.Count)];
            }

            SaveCurrentLevel(level, tiles);
            return tiles;
        }

        private static void ClearCurrentLevelInfo()
        {
            PlayerPrefs.DeleteKey(LastRequestedLevelKey);
            PlayerPrefs.DeleteKey(LastRequestedLevelListKey);
        }

        /// <summary>
        /// Bir kere rastgele level oluşturulduğunda onu kaydet, daha sonra level geçilemezse oluşturulan bu rastgele level yeniden getirilecek.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="tiles"></param>
        private static void SaveCurrentLevel(int level, Tile[] tiles)
        {
            PlayerPrefs.SetInt(LastRequestedLevelKey, level);
            PlayerPrefList.AddOrUpgradeList(LastRequestedLevelListKey, tiles.Select(x => x.Id).ToList());
        }

        private Tile GetTileByIdOrDefault(int id, int level)
        {
            var tile = _allMapTiles.FirstOrDefault(x => x.Id == id);
            //bir hata olursa başka bir tile dön
            return tile == null 
                ? _allMapTiles.FirstOrDefault(t => t.minRequiredPlayerLevel <= level) 
                : tile;
        }
    }
}