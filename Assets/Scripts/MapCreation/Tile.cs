﻿using Collectables;
using UnityEngine;

namespace MapCreation
{
    /// <summary>
    /// Harita parçası. Oyun başlarken rastgele bir çok parça bir araya gelip bir leveli oluşturuyor.
    /// </summary>
    public class Tile : MonoBehaviour
    {
        [SerializeField] private int id;
        public int Id => id;

        public float length;
        public bool usableOnInitialMap = true;//bazı parçalar player tam boydayken playerın ölümüne neden olacağı için onları oyunun başlangıcında kullanamayız

        public EnumCollectableType collectableType;

        private void OnDrawGizmos()
        {
            Vector3 center = transform.position + (length / 2 * Vector3.up);
            Vector3 size = new Vector3(20, length, 20);

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(center, size);
        }
    }
}