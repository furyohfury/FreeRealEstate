using Game.Scripts.Shooter;
using Unity.Netcode;

namespace Game
{
    public sealed class SessionSystem
    {
        private readonly PlayerSpawnSystem _playerSpawnSystem;
        private ScoreSystem _scoreSystem;

        public SessionSystem(PlayerSpawnSystem playerSpawnSystem, ScoreSystem scoreSystem)
        {
            _playerSpawnSystem = playerSpawnSystem;
            _scoreSystem = scoreSystem;
        }

        public void LaunchNextRound()
        {
            if (!NetworkManager.Singleton.IsServer)
                return;

            foreach (var player in _playerSpawnSystem.Players)
            {
                player.GetToSpawnPosition();

                player.Health.Value = player.MaxHealth.Value;
            }
        }

        public void AddPoints(Player player, int points)
        {
            _scoreSystem.AddPoints(player, points);
        }
    }
}
