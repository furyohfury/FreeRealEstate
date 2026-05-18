using Unity.Netcode;
using UnityEngine;

namespace Game.Scripts.Shooter
{
    public sealed class MoveForwardComponent : NetworkBehaviour // TODO process layers
    {
        [SerializeField]
        private float _speed = 10f;
        [SerializeField]
        private Rigidbody _rigidbody;
        [SerializeField]
        private float _stickDistance = 0.1f;

        private void FixedUpdate()
        {
            if (!IsServer)
            {
                return;
            }

            _rigidbody.MovePosition(transform.position + transform.forward * (_speed * Time.fixedDeltaTime));
        }
    }
}