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
            if (!_canMove || GameManager.Instance.player.IsDead)
            {
                _rigidbody.velocity = Vector3.zero;
                return;
            }

            _rigidbody.velocity = _velocityVector;
        }

        private void SetCanMove()
        {
            _canMove = true;
        }

        private void StopWithDelay()
        {
            SimpleTimer.Create(3f, () =>
            {
                if (GameManager.Instance.GameState == GameState.GAME || GameManager.Instance.GameState == GameState.MENU)
                {
                    return;
                }

                _canMove = false;
            });
        }
        
        private void OnEnable()
        {
            GlobalEvents.OnGameEnd += StopWithDelay;
            GlobalEvents.OnGameStart += SetCanMove;
            GlobalEvents.OnSetMenu += SetCanMove;
        }

      
        private void OnDisable()
        {
            GlobalEvents.OnGameEnd -= StopWithDelay;
            GlobalEvents.OnGameStart -= SetCanMove;
            GlobalEvents.OnSetMenu -= SetCanMove;
        }
    }
}