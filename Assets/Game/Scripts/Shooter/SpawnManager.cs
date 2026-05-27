using System;
using Game.Scripts.Shooter;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Game
{
    public sealed class SpawnManager : IInitializable, IDisposable
    {
        public event Action<ulong> OnPlayerNetworkObjectSpawned;

        private readonly Player _playerPrefab;
        private readonly SpawnPoint[] _spawnPoints;
        private int _nextSpawnIndex = 0;
        private readonly DiContainer _container;
        private ZenjectPlayerPrefabInstanceHandler _playerPrefabInstanceHandler;

        public SpawnManager(SpawnPoint[] spawnPoints, Player playerPrefab, DiContainer container)
        {
            _container = container;
            _spawnPoints = spawnPoints;
            _playerPrefab = playerPrefab;
        }

        public void Initialize()
        {
            if (NetworkManager.Singleton != null)
            {
                NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;

                // Регистрируем хендлер для клиентов. 
                // Теперь каждый раз, когда клиент получает команду от сервера заспавнить этот префаб,
                // Netcode на клиенте автоматически вызовет наш ZenjectPlayerPrefabInstanceHandler.
                var playerNetObject = _playerPrefab.GetComponent<NetworkObject>();
                _playerPrefabInstanceHandler = new ZenjectPlayerPrefabInstanceHandler(_container, _playerPrefab);
                NetworkManager.Singleton.PrefabHandler.AddHandler(playerNetObject, _playerPrefabInstanceHandler);
            }
        }

        private void OnClientConnected(ulong clientId)
        {
            if (!NetworkManager.Singleton.IsServer)
                return;

            Transform spawnPoint = _spawnPoints[_nextSpawnIndex].transform;
            _nextSpawnIndex = (_nextSpawnIndex + 1) % _spawnPoints.Length;

            // НА СЕРВЕРЕ: Явно вызываем наш хендлер.
            // Он сделает Object.Instantiate + Zenject-инжекцию по точно такому же пути,
            // по которому пойдет клиент. Никакого дублирования кода!
            NetworkObject networkObject = _playerPrefabInstanceHandler.Instantiate(clientId, spawnPoint.position, spawnPoint.rotation);

            // Передаем объект в сеть
            networkObject.SpawnAsPlayerObject(clientId);

            NotifyPlayerNetworkObjectSpawnedRpc(networkObject.NetworkObjectId);
        }

        [Rpc(SendTo.ClientsAndHost)]
        private void NotifyPlayerNetworkObjectSpawnedRpc(ulong networkObjectId)
        {
            OnPlayerNetworkObjectSpawned?.Invoke(networkObjectId);
        }

        public void Dispose()
        {
            if (NetworkManager.Singleton != null)
            {
                NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;

                if (NetworkManager.Singleton.PrefabHandler != null)
                {
                    var playerNetObject = _playerPrefab.GetComponent<NetworkObject>();
                    NetworkManager.Singleton.PrefabHandler.RemoveHandler(playerNetObject);
                }
            }
        }
    }
}
