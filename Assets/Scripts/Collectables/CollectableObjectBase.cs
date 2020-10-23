using Managers;
using UnityEngine;

namespace Collectables
{
    /// <summary>
    /// Collectable objeler Collectable layerında ve bu layer da yalnızca player ile ile etkileşime geçebilir. Layer Collusion Matrixinden bu şekilde ayarlandı.
    /// </summary>
    public abstract class CollectableObjectBase : MonoBehaviour
    {
        private bool _isCollected = false;

        private void OnTriggerEnter(Collider other)
        {
            if (_isCollected || !other.CompareTag("Player"))
            {
                return;
            }

            _isCollected = true;
            GameManager.Instance.CurrentCollectedItemCount++;

            OnCollect();

            SimpleTimer.Create(2f, DestroyMe);
        }

        private void DestroyMe()
        {
            Destroy(gameObject);
        }

        protected abstract void OnCollect();
    }
}