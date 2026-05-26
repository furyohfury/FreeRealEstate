using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

namespace Game
{
    public sealed class SpawnPositionComponent : NetworkBehaviour
    {
        [SerializeField]
        private NetworkTransform _networkTransform;
        private bool _initialized;
        private Vector3 _position;
        private Quaternion _rotation;

        [Rpc(SendTo.ClientsAndHost)]
        public void GetToSpawnPositionRpc(Vector3 position, Quaternion rotation)
        {
            if (!_initialized && IsOwner)
            {
                _position = position;
                _rotation = rotation;
                _initialized = true;
                _networkTransform.Teleport(position, rotation, Vector3.one);
            }
        }

        [Rpc(SendTo.ClientsAndHost)]
        public void GetToSpawnPosition()
        {
            if (IsOwner)
            {
                _networkTransform.Teleport(_position, _rotation, Vector3.one);
            }
        }
    }
}
