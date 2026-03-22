using System.Threading;
using Game.EngineData;
using TriInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public sealed class LaneItemSpawner : MonoBehaviour
    {
        [SerializeField]
        private Lane _lane;

        [Header("Spawn Settings")]
        [field: SerializeField]
        public float SpawnInterval { get; set; } = 3f;
        [field: SerializeField]
        public float RandomSpawnOffset { get; set; } = 1.5f;
        [field: SerializeField]
        public float RandomAngleOffset { get; set; } = 30f;
        [SerializeField]
        private LayerMask _itemsMask = Layers.ITEM;

        private bool _isSpawning;
        private float _timer;
        private readonly Collider[] _collisions = new Collider[3];
        private CancellationTokenSource _cts;
        private Vector3 _checkSize;

        [Button]
        public void SwitchSpawnState(bool isSpawning)
        {
            if (isSpawning == _isSpawning)
                return;

            _isSpawning = isSpawning;

            if (isSpawning)
            {
                _cts = new CancellationTokenSource();
                StartSpawningAsync(_cts.Token);
            }
            else
            {
                _cts.Cancel();
            }
        }

        private async Awaitable StartSpawningAsync(CancellationToken ctsToken)
        {
            while (!ctsToken.IsCancellationRequested)
            {
                if (_timer <= 0)
                {
                    GameColor[] colors = GameParamsService.Instance.SessionParams.GameColors;
                    var randomColor = colors[Random.Range(0, colors.Length)];
                    Quaternion rotation = _lane.SpawnRot * Quaternion.Euler(0, Random.Range(-RandomAngleOffset, RandomAngleOffset), 0);
                    Item newItem = ItemSystem.Instance.SpawnItemAtLane(_lane.SpawnPos, rotation, _lane);
                    Vector3 rememberedSize = newItem.Collider.bounds.extents * 1.05f;

                    if (Collides(newItem, rememberedSize)) // передаем размер
                    {
                        Debug.Log($"<color=red>Spawned item collides, awaiting...</color>");
                        newItem.gameObject.SetActive(false);
                        await WaitForFreeSpaceAsync(newItem, rememberedSize, ctsToken);
                        newItem.transform.position = _lane.SpawnPos;
                    }

                    newItem.gameObject.SetActive(true);
                    newItem.SetColor(randomColor);
                    _timer = SpawnInterval + Random.Range(-RandomSpawnOffset, RandomSpawnOffset);
                }

                _timer -= Time.deltaTime;

                await Awaitable.NextFrameAsync(ctsToken);
            }
        }

        private bool Collides(Item newItem, Vector3 rememberedSize)
        {
            int collisionsCount = Physics.OverlapBoxNonAlloc(
                _lane.SpawnPos,
                rememberedSize,
                _collisions,
                newItem.transform.rotation,
                _itemsMask);

            for (int i = 0; i < collisionsCount; i++)
            {
                if (_collisions[i] != newItem.Collider)
                {
                    return true; // Нашли кого-то другого — значит коллизия есть
                }
            }

            return false;
        }

        private async Awaitable WaitForFreeSpaceAsync(Item newItem, Vector3 rememberedSize, CancellationToken ctsToken)
        {
            do
            {
                await Awaitable.NextFrameAsync(ctsToken);
            } while (Collides(newItem, rememberedSize));

            _checkSize = Vector3.zero;
        }

        private void OnDrawGizmos()
        {
            if (_checkSize != Vector3.zero)
            {
                Gizmos.DrawWireCube(_lane.SpawnPos, _checkSize);
            }
        }
    }
}
