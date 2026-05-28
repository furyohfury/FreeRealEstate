using Game.Scripts.Shooter;
using UnityEngine;

namespace Game
{
    public sealed class PlayerFactory
    {
        private readonly SpawnPoint[] _spawnPoints;
        private int _nextSpawnIndex = 0;
        private readonly ZenjectNetworkObjectSpawner _prefabInstanceHandler;
        private readonly Player _playerPrefab;

        public PlayerFactory(SpawnPoint[] spawnPoints, ZenjectNetworkObjectSpawner playerInstanceHandler, Player playerPrefab)
        {
            _spawnPoints = spawnPoints;
            _prefabInstanceHandler = playerInstanceHandler;
            _playerPrefab = playerPrefab;
        }

        public Player SpawnPlayer(ulong clientId)
        {
            Transform spawnPoint = _spawnPoints[_nextSpawnIndex].transform;
            _nextSpawnIndex = (_nextSpawnIndex + 1) % _spawnPoints.Length;
            var player = _prefabInstanceHandler.SpawnPlayerPrefab(clientId,
                _playerPrefab,
                spawnPoint.position,
                spawnPoint.rotation);

            return player;
        }
    }
}
