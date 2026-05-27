using System;
using System.Collections.Generic;
using Game.Scripts.Shooter;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Game
{
    public sealed class PlayerSpawnSystem : IInitializable, IDisposable
    {
        public event Action<Player> OnPlayerSpawned;
        public IReadOnlyCollection<Player> Players => _players;
        private readonly Player _playerPrefab;

        private readonly HashSet<Player> _players = new HashSet<Player>();
        private readonly SpawnPoint[] _spawnPoints;
        private int _nextSpawnIndex = 0;
        private readonly DiContainer _container;
        private ZenjectPlayerPrefabInstanceHandler _playerPrefabInstanceHandler;

        public PlayerSpawnSystem(SpawnPoint[] spawnPoints, Player playerPrefab, DiContainer container)
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
                NetworkObject playerNetObject = _playerPrefab.GetComponent<NetworkObject>();
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
            NetworkObject networkObject = _playerPrefabInstanceHandler.Instantiate(clientId, spawnPoint.position, spawnPoint.rotation);
            networkObject.SpawnAsPlayerObject(clientId);

            if (networkObject.TryGetComponent(out Player player))
            {
                _players.Add(player);
            }

            NotifyPlayerNetworkObjectSpawnedRpc(networkObject.NetworkObjectId);
        }

        [Rpc(SendTo.ClientsAndHost)]
        private void NotifyPlayerNetworkObjectSpawnedRpc(ulong networkObjectId)
        {
            IReadOnlyList<NetworkObject> playerObjects = NetworkManager.Singleton.SpawnManager.PlayerObjects;

            for (int i = 0, count = playerObjects.Count; i < count; i++)
            {
                if (playerObjects[i].NetworkObjectId == networkObjectId && playerObjects[i]
                        .TryGetComponent(out Player player))
                {
                    OnPlayerSpawned?.Invoke(player);
                }
            }
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
