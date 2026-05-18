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
            TurnOffMoveForwardRpc();
        }

        [Rpc(SendTo.ClientsAndHost)]
        private void TurnOffMoveForwardRpc()
        {
            _moveForwardComponent.enabled = false;
        }
    }
}
