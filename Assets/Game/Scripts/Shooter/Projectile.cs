using Unity.Netcode;
using UnityEngine;

namespace Game.Scripts.Shooter
{
    public sealed class Projectile : NetworkBehaviour
    {
        public ulong ShooterNetworkId { get; private set; }
        [SerializeField]
        private MoveForwardComponent _moveForwardComponent;
        [SerializeField]
        private StickOnTriggerComponent _stickOnTriggerComponent;
        [SerializeField]
        private LifetimeComponentNet _lifetimeComponent;
        [SerializeField]
        private DealDamageComponent _dealDamageComponent;
        private bool _hit;

        public void Initialize(ulong shooterNetworkObjectId)
        {
            ShooterNetworkId = shooterNetworkObjectId;
        }

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
                _dealDamageComponent.DealDamage(healthComponent, ShooterNetworkId);
            }

            _stickOnTriggerComponent.OnTrigger(other);
        }
    }
}
