using System.Linq;
using UnityEngine;
using Utilities.PlayerInfoStore;

namespace MapCreation
{
    public class RoadManager : MonoBehaviour
    {
        [SerializeField] private int minTileLengthInALevel;
        [SerializeField] private int maxTileLengthInALevel;

        [Header("Prefabs")] [SerializeField] private Tile[] allMapTiles;
        [SerializeField] private Tile finishTile;
        public Tile[] GetAllMapTiles => allMapTiles;

        private void SpawnLevel()
        {
            CleanTiles();

            var levelTiles = GetRandomTiles();

            Vector3 spawnPos = Vector3.zero;

            //harita elemanlarının içerisinde dön ve onları oluştur
            foreach (var tile in levelTiles)
            {
                var newTile = Instantiate(tile, spawnPos, Quaternion.identity, transform);
                newTile.gameObject.SetActive(false);

                spawnPos.y += tile.length;
            }

            var finish = Instantiate(finishTile, spawnPos, Quaternion.identity, transform);
            finish.gameObject.SetActive(false);
        }

        private void CleanTiles()
        {
            // Hali hazırda oluşturulmuş bir harita varsa temizle
            while (transform.childCount > 0)
            {
                Transform t = transform.GetChild(0);
                t.SetParent(null);
                Destroy(t.gameObject);
            }
        }

        private Tile[] GetRandomTiles()
        {
            var playersCollectableType = PlayerCollectableTypeInfoStore.Get();
            var availableTiles = allMapTiles.Where(t => t.collectableType == playersCollectableType).ToList();

            int tileCountInLevel = Random.Range(minTileLengthInALevel, maxTileLengthInALevel + 1);

            var tiles = new Tile[tileCountInLevel];
            for (int i = 0; i < tileCountInLevel; i++)
            {
                tiles[i] = availableTiles[Random.Range(0, availableTiles.Count)];
            }

            return tiles;
        }

        private void ActivateMap()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform t = transform.GetChild(i);
                t.position = InitialMapManager.GetCurrentSpawnPosition + t.position;
                t.gameObject.SetActive(true);
            }
        }

        private void OnEnable()
        {
            GlobalEvents.OnSetMenu += SpawnLevel;
            GlobalEvents.OnCollectableTypeChange += SpawnLevel;
            GlobalEvents.OnGameStart += ActivateMap;
        }

        private void OnDisable()
        {
            GlobalEvents.OnSetMenu -= SpawnLevel;
            GlobalEvents.OnCollectableTypeChange -= SpawnLevel;
            GlobalEvents.OnGameStart -= ActivateMap;
        }
    }
}