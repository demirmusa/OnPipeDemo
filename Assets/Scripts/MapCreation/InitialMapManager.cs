using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;
using Utilities.PlayerInfoStore;

namespace MapCreation
{
    /// <summary>
    /// Oyun başlamadan önce sonsuz döndüdeki haritayı oluşturur. Oyun başladığında oluşturulacak harita bu haritanın peşine oluşacaktır. 
    /// </summary>
    public class InitialMapManager : MonoBehaviour
    {
        [SerializeField] private float minLength;
        [SerializeField] private float updateDelay = 1f;

        private Queue<Tile> initialTilesQueue = new Queue<Tile>();

        private static Vector3 spawnPos;
        public static Vector3 GetCurrentSpawnPosition => spawnPos;

        private float timer;
        private bool _initialMapActive = false;

        private void Start()
        {
            SpawnLevel();
            timer = updateDelay;
        }

        private void SpawnLevel()
        {
            timer = updateDelay;
            spawnPos = Vector3.up * -2;
            
            CleanTiles();
            FillQueue();
            _initialMapActive = true;
        }

        private void Update()
        {
            if (!_initialMapActive)
            {
                return;
            }

            if (timer > 0)
            {
                timer -= Time.deltaTime;
                return;
            }

            timer = updateDelay;
            UpdateInitialMap();
        }

        private void UpdateInitialMap()
        {
            //ilk elemanı al, haritada en son noktaya at, kuyruktada en sona ata.
            //Bu sayede basit bir object pooling mantığı ile sonsuz döngü bir kaç tane gameobject ile oluşturuluyor.
            var tile = initialTilesQueue.Dequeue();
            tile.transform.position = spawnPos;
            spawnPos.y += tile.length;
            initialTilesQueue.Enqueue(tile);
        }

        private void CleanTiles()
        {
            // Hali hazırda oluşturulmuş bir harita varsa temizle
            initialTilesQueue.Clear();
            while (transform.childCount > 0)
            {
                Transform t = transform.GetChild(0);
                t.SetParent(null);
                Destroy(t.gameObject);
            }
        }

        private void FillQueue()
        {
            var playersCollectableType = PlayerCollectableTypeInfoStore.Get();
            var availableTiles = GameManager.Instance.roadManager.GetAllMapTiles.Where(t => t.collectableType == playersCollectableType && t.usableOnInitialMap).ToList();

            while (spawnPos.y < minLength)
            {
                var tile = availableTiles[Random.Range(0, availableTiles.Count)];
                var tileGO = Instantiate(tile, spawnPos, Quaternion.identity, transform);
                tileGO.gameObject.SetActive(true);
                initialTilesQueue.Enqueue(tileGO);
                spawnPos.y += tile.length;
            }
        }

        private void OnGameStart()
        {
            _initialMapActive = false;
        }

        private void OnEnable()
        {
            GlobalEvents.OnSetMenu += SpawnLevel;
            GlobalEvents.OnCollectableTypeChange += SpawnLevel;
            GlobalEvents.OnGameStart += OnGameStart;
        }

        private void OnDisable()
        {
            GlobalEvents.OnSetMenu -= SpawnLevel;
            GlobalEvents.OnCollectableTypeChange -= SpawnLevel;
            GlobalEvents.OnGameStart -= OnGameStart;
        }
    }
}