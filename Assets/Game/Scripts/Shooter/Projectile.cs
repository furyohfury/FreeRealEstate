using Unity.Netcode;
using UnityEngine;

namespace Game.Scripts.Shooter
{
    public sealed class Projectile : NetworkBehaviour
    {
        [SerializeField]
        private MoveForwardComponent _moveForwardComponent;
        [SerializeField]
        private StickOnTriggerComponent _stickOnTriggerComponent;
        [SerializeField]
        private LifetimeComponentNet _lifetimeComponent;
        [SerializeField]
        private DealDamageComponent _dealDamageComponent;

        private void OnTriggerEnter(Collider other)
        {
            if (IsServer == false)
            {
                return;
            }

            Debug.Log($"<color=green>projectile OnTriggerEnter with {other.gameObject.name}</color>");

            if (other.TryGetComponent<IHealth>(out IHealth health))
            {
                _dealDamageComponent.DealDamage(health);
            }

            _stickOnTriggerComponent.OnTrigger(other);
        }
    }
}
