using Unity.Netcode;
using UnityEngine;

namespace Game.Scripts.Shooter
{
    public sealed class ProjectileComponent : NetworkBehaviour
    {
        [SerializeField]
        private float _speed = 10f;
        [SerializeField]
        private Rigidbody _rigidbody;
        [SerializeField]
        private float _lifeTime = 10f;
        [SerializeField]
        private float _damage = 1f;

        public override void OnNetworkSpawn()
        {
            if (IsServer)
            {
                Destroy(gameObject, _lifeTime);
                // TODO process layers
            }
        }

        private void FixedUpdate()
        {
            if (!IsServer)
            {
                return;
            }

            _rigidbody.MovePosition(transform.position + transform.forward * (_speed * Time.fixedDeltaTime));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsServer)
            {
                return;
            }

            if (other.TryGetComponent<IHealth>(out IHealth health))
            {
                health.TakeDamage(_damage);
            }

            // GetComponent<NetworkObject>().Despawn();
            // stick to surface
        }
    }
}