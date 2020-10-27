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

        public void SpawnLevel()
        {
            CleanTiles();

            var levelTiles = GetRandomTiles();

            Vector3 spawnPos = Vector3.zero;

            //harita elemanlarının içerisinde dön ve onları oluştur
            foreach (var tile in levelTiles)
            {
                Instantiate(tile, spawnPos, Quaternion.identity, transform);
                spawnPos.y += tile.length;
            }

            Instantiate(finishTile, spawnPos, Quaternion.identity, transform);
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
            var availableTiles = allMapTiles.Where(t => t.CollectableType == playersCollectableType).ToList();

            int tileCountInLevel = Random.Range(minTileLengthInALevel, maxTileLengthInALevel + 1);

            var tiles = new Tile[tileCountInLevel];
            for (int i = 0; i < tileCountInLevel; i++)
            {
                tiles[i] = availableTiles[Random.Range(0, availableTiles.Count)];
            }

            return tiles;
        }

        private void OnEnable()
        {
            GlobalEvents.OnSetMenu += SpawnLevel;
        }

        private void OnDisable()
        {
            GlobalEvents.OnSetMenu -= SpawnLevel;
        }
    }
}