using System;
using Game.Scripts.Shooter;
using Unity.Netcode;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Game
{
    public sealed class SpawnManager : IInitializable, IDisposable
    {
        private readonly Player _playerPrefab;
        private readonly SpawnPoint[] _spawnPoints;
        private int _nextSpawnIndex = 0;
        private readonly DiContainer _container;

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
                // Подписываемся на событие подключения клиента
                NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
            }
        }

        private void OnClientConnected(ulong clientId)
        {
            // Спавнить имеет право ТОЛЬКО сервер
            if (!NetworkManager.Singleton.IsServer)
                return;

            // Выбираем точку спавна
            Transform spawnPoint = _spawnPoints[_nextSpawnIndex].transform;
            _nextSpawnIndex = (_nextSpawnIndex + 1) % _spawnPoints.Length;

            // Инстанцируем префаб в нужных координатах
            var playerInstance = Object.Instantiate(_playerPrefab, spawnPoint.position, spawnPoint.rotation);
            _container.InjectGameObject(playerInstance.gameObject);

            // Передаем объект в сеть и назначаем ему владельца (clientId)
            playerInstance.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);

            if (playerInstance.TryGetComponent<SpawnPositionComponent>(out var getToSpawnPositionComponent))
            {
                getToSpawnPositionComponent.GetToSpawnPositionRpc(spawnPoint.position, spawnPoint.rotation);
            }
        }

        public void Dispose()
        {
            if (NetworkManager.Singleton != null)
            {
                // Подписываемся на событие подключения клиента
                NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
            }
        }
    }
}
