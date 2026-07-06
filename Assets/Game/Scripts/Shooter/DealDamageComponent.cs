using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Shooter
{
    public sealed class DealDamageComponent : NetworkBehaviour
    {
        [SerializeField]
        private float _damage = 1f;
        private DealDamageSystem _dealDamageSystem;

        [Inject]
        public void Construct(DealDamageSystem dealDamageSystem)
        {
            _dealDamageSystem = dealDamageSystem;
        }

        public void DealDamage(IHealth health)
        {
            _dealDamageSystem.DealDamage(new DealDamageEvent(health.OwnerClientId, _damage, OwnerClientId), health);
        }
    }
}
