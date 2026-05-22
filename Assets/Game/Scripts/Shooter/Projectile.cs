using Unity.Netcode;
using UnityEngine;

namespace Game.Scripts.Shooter
{
    public sealed class Projectile : NetworkBehaviour
    {
        [SerializeField]
        private MoveForwardComponent _moveForwardComponent;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"<color=green>projectile OnTriggerEnter with {other.gameObject.name}</color>");
            TurnOffMoveForwardRpc();
        }

        [Rpc(SendTo.ClientsAndHost)]
        private void TurnOffMoveForwardRpc()
        {
            _moveForwardComponent.enabled = false;
        }
    }
}
