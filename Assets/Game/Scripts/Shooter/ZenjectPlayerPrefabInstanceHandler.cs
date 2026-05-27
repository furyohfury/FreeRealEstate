using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Shooter
{
    public class ZenjectPlayerPrefabInstanceHandler : INetworkPrefabInstanceHandler
    {
        private readonly DiContainer _container;
        private readonly NetworkObject _playerPrefabNetworkObject;

        public ZenjectPlayerPrefabInstanceHandler(DiContainer container, Player playerPrefab)
        {
            _container = container;
            _playerPrefabNetworkObject = playerPrefab.GetComponent<NetworkObject>();
        }

        // Вызывается Netcode и на сервере, и на клиенте при спавне объекта в сети
        public NetworkObject Instantiate(ulong ownerClientId, Vector3 position, Quaternion rotation)
        {
            GameObject playerGameObject = Object.Instantiate(_playerPrefabNetworkObject.gameObject, position, rotation);

            // 2. Вручную заставляем Zenject проинжектить зависимости во все компоненты (и GameObjectContext)
            _container.InjectGameObject(playerGameObject);

            return playerGameObject.GetComponent<NetworkObject>();
        }

        // Вызывается Netcode, когда объект деспавнится
        public void Destroy(NetworkObject networkObject)
        {
            Object.Destroy(networkObject.gameObject);
        }
    }
}
