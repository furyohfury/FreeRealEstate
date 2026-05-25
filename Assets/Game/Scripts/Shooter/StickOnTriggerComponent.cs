using Unity.Netcode;
using UnityEngine;

namespace Game.Scripts.Shooter
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class StickOnTriggerComponent : NetworkBehaviour
    {
        [SerializeField]
        private float _stickDistance = 0.3f;
        private bool _isCollided;

        private void OnTriggerEnter(Collider other)
        {
            if (IsServer == false
                || _isCollided
                || other.TryGetComponent(out Player _))
            {
                return;
            }

            transform.position += transform.forward * _stickDistance;
            _isCollided = true;
        }
    }
}
