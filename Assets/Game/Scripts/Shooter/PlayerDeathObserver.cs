using System;
using System.Collections.Generic;
using Game.Scripts.Shooter;
using Unity.Netcode;
using Zenject;

namespace Game
{
    public sealed class PlayerDeathObserver : IInitializable, IDisposable
    {
        private readonly SpawnManager _spawnManager;
        private readonly HashSet<Player> _players = new HashSet<Player>();

        public PlayerDeathObserver(SpawnManager spawnManager)
        {
            _spawnManager = spawnManager;
        }

        public void Initialize()
        {
            _spawnManager.OnPlayerNetworkObjectSpawned += SpawnManagerOnOnPlayerNetworkObjectSpawned;
        }

        private void SpawnManagerOnOnPlayerNetworkObjectSpawned(ulong obj)
        {
            NetworkObject networkObject = NetworkManager.Singleton.SpawnManager.SpawnedObjects[obj];
            var player = networkObject.GetComponent<Player>();
            player.Health.OnValueChanged += OnHealthChanged;
            _players.Add(player);
        }

        private void OnHealthChanged(float previousValue, float newValue)
        {
            if (newValue <= 0)
            {
                LaunchNextRound();
            }
        }

        private void LaunchNextRound()
        {
            foreach (var player in _players)
            {
                player.GetToSpawnPosition();

                if (NetworkManager.Singleton.IsServer)
                {
                    player.Health.Value = player.MaxHealth.Value;
                }
            }
        }

        public void Dispose()
        {
            _spawnManager.OnPlayerNetworkObjectSpawned += SpawnManagerOnOnPlayerNetworkObjectSpawned;

            foreach (var player in _players)
            {
                player.Health.OnValueChanged -= OnHealthChanged;
            }
        }
    }
}
