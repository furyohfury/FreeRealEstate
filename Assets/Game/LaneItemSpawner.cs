using TriInspector;
using UnityEngine;

namespace Game
{
    public sealed class LaneItemSpawner : MonoBehaviour
    {
        [SerializeField] private ItemSystem _itemSystem;
        [SerializeField] private Lane _lane;
        [SerializeField] private GameParams _gameParams;

        [Header("Spawn Settings")]
        [SerializeField] private float _spawnInterval;
        [SerializeField] private float _randomSpawnOffset;
        [SerializeField] private float _randomAngleOffset;

        private bool _isSpawning;
        private float _timer;

        [Button]
        public void StartSpawning()
        {
            _isSpawning = true;
        }

        private void Update()
        {
            if (_isSpawning)
            {
                if (_timer <= 0)
                {
                    var randomColor = _gameParams.Colors[Random.Range(0, _gameParams.Colors.Length)];
                    Quaternion rotation = _lane.SpawnRot * Quaternion.Euler(0, Random.Range(-_randomAngleOffset, _randomAngleOffset), 0);
                    var newItem = _itemSystem.SpawnItem(_lane.SpawnPos, rotation, _lane);
                    newItem.SetColor(randomColor);
                    _lane.AddItem(newItem);
                    _timer = _spawnInterval + Random.Range(-_randomSpawnOffset, _randomSpawnOffset);
                }

                _timer -= Time.deltaTime;
            }
        }
    }
}
