using Unity.Netcode;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Game.Scripts.Shooter
{
    public class ZenjectPrefabInstanceHandler : INetworkPrefabInstanceHandler
    {
        private readonly DiContainer _container;
        private readonly NetworkObject _networkObject;

        public ZenjectPrefabInstanceHandler(DiContainer container, NetworkObject networkObject)
        {
            _container = container;
            _networkObject = networkObject;
        }

        public NetworkObject GetNetworkObject()
        {
            return _networkObject;
        }

        // Вызывается Netcode и на сервере, и на клиенте при спавне объекта в сети
        public NetworkObject Instantiate(ulong ownerClientId, Vector3 position, Quaternion rotation)
        {
            var networkObject = Object.Instantiate(_networkObject, position, rotation);

            // 2. Вручную заставляем Zenject проинжектить зависимости во все компоненты (и GameObjectContext)
            _container.InjectGameObject(networkObject.gameObject);

            return networkObject;
        }

        // Вызывается Netcode, когда объект деспавнится
        public void Destroy(NetworkObject networkObject)
        {
            Object.Destroy(networkObject.gameObject);
        }
    }
}
