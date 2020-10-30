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
            Debug.Log(GameManager.Instance.player.IsDead);
            Debug.Log(GameManager.Instance.GameState.ToString());
            
            if (GameManager.Instance.player.IsDead || GameManager.Instance.GameState != GameState.GAME && GameManager.Instance.GameState != GameState.MENU)
            {
                _rigidbody.velocity = Vector3.zero;
                return;
            }
            Debug.Log("Move");
            _rigidbody.velocity = _velocityVector;
        }
    }
}