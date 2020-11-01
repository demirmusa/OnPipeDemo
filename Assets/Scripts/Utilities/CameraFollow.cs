using UnityEngine;

namespace Utilities
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        private Vector3 _offset;

        private bool canFollow = false;

        private void Start()
        {
            _offset = _target.transform.position - transform.position;
        }

        private void Update()
        {
            if (!canFollow)
            {
                return;
            }

            transform.position = _target.position - _offset;
        }

        private void StopFollow()
        {
            canFollow = false;
        }

        private void StartFollow()
        {
            canFollow = true;
        }

        private void OnEnable()
        {
            GlobalEvents.OnGameStart += StartFollow;
            GlobalEvents.OnSetMenu += StartFollow;
            GlobalEvents.OnGameEnd += StopFollow;
        }

        private void OnDisable()
        {
            GlobalEvents.OnGameStart -= StartFollow;
            GlobalEvents.OnSetMenu -= StartFollow;
            GlobalEvents.OnGameEnd -= StopFollow;
        }
    }
}