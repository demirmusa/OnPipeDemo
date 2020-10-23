using System;
using UnityEngine;

namespace MapCreation
{
    public class RoadManager : MonoBehaviour
    {
        [SerializeField] private int minTileLengthInALevel;
        [SerializeField] private int maxTileLengthInALevel;

        [Header("Prefabs")] [SerializeField] private Tile[] allMapTiles;
        [SerializeField] private Tile finishTile;

        private LevelManager _levelManager;

        private void Awake()
        {
            _levelManager = new LevelManager(minTileLengthInALevel, maxTileLengthInALevel, allMapTiles);
        }

        public void SpawnLevel()
        {
            CleanTiles();

            //level managerdan şuanki levele ait haritayı iste
            var levelTiles = _levelManager.GetLevelTiles();

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