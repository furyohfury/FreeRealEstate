using System;
using TriInspector;
using Unity.Netcode;
using UnityEngine;

namespace Game.Scripts.Shooter
{
    public sealed class HealthComponent : NetworkBehaviour
    {
        [field: SerializeField]
        public NetworkVariable<float> Health { get; private set; } =
            new NetworkVariable<float>(10f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        [field: SerializeField]
        public NetworkVariable<float> MaxHealth { get; private set; } =
            new NetworkVariable<float>(10f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [Button]
        public void TakeDamage(float damage)
        {
            Debug.Log($"{this.name} take {damage} damage");
            Health.Value = Mathf.Max(0, Health.Value - damage);
        }
    }
}
