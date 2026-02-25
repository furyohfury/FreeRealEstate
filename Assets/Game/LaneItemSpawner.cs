using TriInspector;
using UnityEngine;

namespace Game
{
    public sealed class LaneItemSpawner : MonoBehaviour
    {
        [SerializeField] private Lane _lane;

        [Header("Spawn Settings")]
        [SerializeField] private float _spawnInterval;
        [SerializeField] private float _randomSpawnOffset;
        [SerializeField] private float _randomAngleOffset;

        private bool _isSpawning;
        private float _timer;

        [Button]
        public void SwitchSpawnState(bool isSpawning)
        {
            _isSpawning = isSpawning;
        }

        private void Update()
        {
            if (_isSpawning)
            {
                if (_timer <= 0)
                {
                    GameColor[] colors = GameParamsService.Instance.SessionParams.GameColors;
                    var randomColor = colors[Random.Range(0, colors.Length)];
                    Quaternion rotation = _lane.SpawnRot * Quaternion.Euler(0, Random.Range(-_randomAngleOffset, _randomAngleOffset), 0);
                    var newItem = ItemSystem.Instance.SpawnItem(_lane.SpawnPos, rotation, _lane);
                    newItem.SetColor(randomColor);
                    _lane.AddItem(newItem);
                    _timer = _spawnInterval + Random.Range(-_randomSpawnOffset, _randomSpawnOffset);
                }

                _timer -= Time.deltaTime;
            }
        }
    }
}
