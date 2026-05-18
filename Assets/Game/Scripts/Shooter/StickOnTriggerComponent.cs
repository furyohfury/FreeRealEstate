using Unity.Netcode;
using UnityEngine;

namespace Game.Scripts.Shooter
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class StickOnTriggerComponent : NetworkBehaviour
    {
        [SerializeField]
        private float _stickDistance = 0.1f;
        private bool _isCollided;
        private Collider _other;
        private Vector3 _offset;
        private Quaternion _rotOffset;

        private void OnTriggerEnter(Collider other)
        {
            if (IsServer == false
                || _isCollided)
            {
                return;
            }

            _other = other;
            transform.position += transform.forward * _stickDistance;
            _offset = transform.position - other.transform.position;
            _rotOffset = Quaternion.Inverse(other.transform.rotation) * transform.rotation;
            _isCollided = true;
        }

        private void Update()
        {
            if (IsServer == false
                || _isCollided == false)
            {
                return;
            }

            if (_other == null)
            {
                NetworkObject.Despawn();
            }

            transform.position = _other.transform.position + _offset;
            transform.rotation = _other.transform.rotation * _rotOffset;
        }
    }
}
