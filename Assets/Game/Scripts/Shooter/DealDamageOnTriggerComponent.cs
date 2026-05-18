using Unity.Netcode;
using UnityEngine;

namespace Game.Scripts.Shooter
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class DealDamageOnTriggerComponent : NetworkBehaviour
    {
        [SerializeField]
        private float _damage = 1f;
        private bool _collided;

        private void OnTriggerEnter(Collider other)
        {
            if (IsServer == false || _collided)
            {
                return;
            }

            _collided = true;

            if (other.TryGetComponent<IHealth>(out IHealth health))
            {
                health.TakeDamage(_damage);
            }
        }
    }
}
