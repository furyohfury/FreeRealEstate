using TriInspector;
using UnityEngine;

namespace Game
{
    public sealed class LaneItemSpawner : MonoBehaviour
    {
        [SerializeField] private Lane _lane;

        [Header("Spawn Settings")]
        [field: SerializeField]
        public float SpawnInterval { get; set; } = 3f;
        [field: SerializeField]
        public float RandomSpawnOffset { get; set; } = 1.5f;
        [field: SerializeField]
        public float RandomAngleOffset { get; set; } = 30f;

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
                    Quaternion rotation = _lane.SpawnRot * Quaternion.Euler(0, Random.Range(-RandomAngleOffset, RandomAngleOffset), 0);
                    var newItem = ItemSystem.Instance.SpawnItemAtLane(_lane.SpawnPos, rotation, _lane);
                    newItem.SetColor(randomColor);
                    _timer = SpawnInterval + Random.Range(-RandomSpawnOffset, RandomSpawnOffset);
                }

                _timer -= Time.deltaTime;
            }
        }
    }
}
