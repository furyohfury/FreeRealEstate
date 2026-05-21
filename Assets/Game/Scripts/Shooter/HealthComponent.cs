using TriInspector;
using Unity.Netcode;
using UnityEngine;

namespace Game.Scripts.Shooter
{
    public sealed class HealthComponent : NetworkBehaviour
    {
        [field: SerializeField]
        public NetworkVariable<float> Health { get; private set; } = new NetworkVariable<float>(10f);
        [field: SerializeField]
        public NetworkVariable<float> MaxHealth { get; private set; } = new NetworkVariable<float>(10f);

        [Button]
        public void TakeDamage(float damage)
        {
            Debug.Log($"{name} take {damage} damage");
            Health.Value = Mathf.Max(0, Health.Value - damage);
        }
    }
}
