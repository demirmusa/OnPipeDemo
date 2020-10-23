using Managers;
using UnityEngine;

namespace Collectables
{
    public abstract class CollectableObjectBase : MonoBehaviour
    {
        private bool _isCollected = false;

        private SimpleTimer _timer;

        private void OnTriggerEnter(Collider other)
        {
            if (_isCollected || !other.CompareTag("Player"))
            {
                return;
            }

            _isCollected = true;
            GameManager.Instance.CurrentCollectedItemCount++;

            OnCollect();

            _timer = SimpleTimer.Create(2f, DestroyMe);
        }

        private void DestroyMe()
        {
            Destroy(gameObject);
        }

        private void OnDisable()
        {
            if (_timer)
            {
                _timer.Stop();
            }
        }

        protected abstract void OnCollect();
    }
}