using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

namespace Game
{
    public sealed class SpawnPositionComponent : NetworkBehaviour
    {
        [SerializeField]
        private NetworkTransform _networkTransform;
        [SerializeField]
        private CharacterController _characterController;
        private bool _initialized;
        private Vector3 _position;
        private Quaternion _rotation;

        public override void OnNetworkSpawn()
        {
            // Нас интересует только владелец (клиент, который управляет этим персонажем)
            if (IsOwner)
            {
                // transform.position и rotation УЖЕ содержат правильные данные, 
                // которые сервер указал при Instantiate на своей стороне. 
                // Netcode автоматически передает их клиенту при спавне.
                Vector3 spawnPosition = transform.position;
                Quaternion spawnRotation = transform.rotation;
                _position = spawnPosition;
                _rotation = spawnRotation;

                // Принудительно телепортируем NetworkTransform владельца в эти координаты,
                // чтобы он зафиксировал их как стартовую точку и не слал (0,0,0) на сервер.
                _networkTransform.Teleport(spawnPosition, spawnRotation, Vector3.one);

                Debug.Log($"[Spawn] Игрок успешно телепортирован в стартовую точку: {spawnPosition}");
            }
        }

        [Rpc(SendTo.ClientsAndHost)]
        public void GetToSpawnPositionRpc()
        {
            if (IsOwner)
            {
                _characterController.enabled = false;
                transform.position = _position;
                transform.rotation = _rotation;
                _networkTransform.Teleport(_position, _rotation, Vector3.one);
                _characterController.enabled = true;
            }
        }
    }
}
