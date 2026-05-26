using Unity.Netcode;
using UnityEngine;

namespace Game.Scripts.Shooter
{
    public sealed class DealDamageComponent : NetworkBehaviour
    {
        [SerializeField]
        private float _damage = 1f;

        public void DealDamage(IHealth health)
        {
            health.TakeDamage(_damage);
        }
    }
}
