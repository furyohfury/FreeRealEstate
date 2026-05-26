using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

namespace Game
{
    public sealed class GetToSpawnPositionComponent : NetworkBehaviour
    {
        private bool _initialized;

        [Rpc(SendTo.ClientsAndHost)]
        public void GetToSpawnPositionRpc(Vector3 position, Quaternion rotation)
        {
            if (!_initialized && IsOwner)
            {
                _initialized = true;
                GetComponent<NetworkTransform>().Teleport(position, rotation, Vector3.one);
            }
        }
    }
}
