using Managers;
using UnityEngine;

namespace Players
{
    public class PlayerMovementManager : MonoBehaviour
    {
        [SerializeField] private float speed;
        private Rigidbody _rigidbody;

        private bool _canMove;
        private Vector3 _velocityVector;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();

            _velocityVector = speed * Vector3.up;
            _rigidbody.velocity = Vector3.zero;
        }

        private void Update()
        {
            if (GameManager.Instance.player.IsDead || GameManager.Instance.GameState != GameState.GAME)
            {
                _rigidbody.velocity = Vector3.zero;
                return;
            }

            _rigidbody.velocity = _velocityVector;
        }
    }
}