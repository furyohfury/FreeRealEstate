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
        private bool _hit;

        private void OnTriggerEnter(Collider other)
        {
            if (_hit || IsServer == false)
            {
                return;
            }

            _hit = true;
            Debug.Log($"<color=green>projectile OnTriggerEnter with {other.gameObject.name}</color>");

            if (other.TryGetComponent<RagdollPartProxy>(out RagdollPartProxy ragdollPartProxy)
                && ragdollPartProxy.NetworkObject.TryGetComponent(out IHealth healthComponent))
            {
                _dealDamageComponent.DealDamage(healthComponent);
            }

            _stickOnTriggerComponent.OnTrigger(other);
        }
    }
}
