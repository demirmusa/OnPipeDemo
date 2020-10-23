using UnityEngine;

namespace Players
{
    public class Player : MonoBehaviour
    {
        private static readonly int DieAnimHash = Animator.StringToHash("Die");
        private static readonly int ResetAnimHash = Animator.StringToHash("Reset");

        public PlayerMovementManager playerMovementManager;
        public PlayerScaleManager playerScaleManager;

        [SerializeField] private Animator playerArchAnimator;

        private Vector3 _startPosition;

        public bool IsDead { get; private set; }

        private void Start()
        {
            _startPosition = transform.position;
        }

        public void Die()
        {
            IsDead = true;
            if (playerArchAnimator != null)
            {
                playerArchAnimator.SetTrigger(DieAnimHash);
            }
        }

        public void Reset()
        {
            IsDead = false;
            transform.position = _startPosition;
            
            if (playerArchAnimator != null)
            {
                playerArchAnimator.SetTrigger(ResetAnimHash);
            }
        }
    }
}