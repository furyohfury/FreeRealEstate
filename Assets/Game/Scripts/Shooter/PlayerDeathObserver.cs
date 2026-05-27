using System;
using Game.Scripts.Shooter;
using Zenject;

namespace Game
{
    public sealed class PlayerDeathObserver : IInitializable, IDisposable
    {
        private readonly RoundManager _roundManager;
        private readonly PlayerSpawnSystem _playerSpawnSystem;

        public PlayerDeathObserver(RoundManager roundManager, PlayerSpawnSystem playerSpawnSystem)
        {
            _roundManager = roundManager;
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
                _roundManager.LaunchNextRound();
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
