using System;
using UnityEngine;

namespace Collectables
{
    public class RigidBodyAnimatedCollectable : CollectableObjectBase
    {
        [SerializeField] private float forceForward = 100;
        [SerializeField] private float forceUpward = 50;
        private static Vector3 _upForce;

        private void Start()
        {
            if (_upForce == default)
            {
                _upForce = Vector3.up * forceUpward;
            }
        }

        protected override void OnCollect()
        {
            var rigids = gameObject.GetComponentsInChildren<Rigidbody>();
            for (var i = 0; i < rigids.Length; i++)
            {
                rigids[i].isKinematic = false;
                rigids[i].useGravity = true;
                rigids[i].AddForce(rigids[i].transform.forward * forceForward + _upForce);
            }
        }
    }
}