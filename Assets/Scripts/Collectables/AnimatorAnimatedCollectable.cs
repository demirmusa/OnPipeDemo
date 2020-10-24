using UnityEngine;

namespace Collectables
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorAnimatedCollectable : CollectableObjectBase
    {
        private static readonly int Collect = Animator.StringToHash("OnCollect");
        
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        protected override void OnCollect()
        {
            _animator.SetTrigger(Collect);
        }
    }
}