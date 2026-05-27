using System;
using Game.Scripts.Shooter;
using Zenject;

namespace Game
{
    public sealed class PlayerDeathObserver : IInitializable, IDisposable
    {
        private readonly SessionSystem _sessionSystem;
        private readonly PlayerSpawnSystem _playerSpawnSystem;

        public PlayerDeathObserver(SessionSystem sessionSystem, PlayerSpawnSystem playerSpawnSystem)
        {
            _sessionSystem = sessionSystem;
            _playerSpawnSystem = playerSpawnSystem;
        }

        public void Initialize()
        {
            _playerSpawnSystem.OnPlayerSpawned += SpawnManagerOnOnPlayerNetworkObjectSpawned;
        }

        private void SpawnManagerOnOnPlayerNetworkObjectSpawned(Player player)
        {
            player.Health.OnValueChanged += HealthOnValueChanged;
        }

        private void HealthOnValueChanged(float previousValue, float newValue)
        {
            if (newValue <= 0)
            {
                _sessionSystem.LaunchNextRound();
            }
        }

        public void Dispose()
        {
            foreach (var player in _playerSpawnSystem.Players)
            {
                player.Health.OnValueChanged -= HealthOnValueChanged;
            }

            _playerSpawnSystem.OnPlayerSpawned -= SpawnManagerOnOnPlayerNetworkObjectSpawned;
        }
    }
}
