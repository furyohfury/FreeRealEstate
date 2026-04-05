using System.Threading;
using DG.Tweening;
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
        [SerializeField]
        private Transform _initialPoint;
        [SerializeField]
        private Transform _itemsSpawnPos;
        [SerializeField]
        private float _pipeItemMoveDuration = 0.5f;
        [SerializeField]
        private float _itemInitialScale = 0.1f;
        [SerializeField]
        private float _changeScaleMoveProgressRatio = 0.85f;
        [SerializeField]
        private Ease _scaleToNormalSizeEase = Ease.InQuint;
        [SerializeField]
        private float _enlargeAnimScale = 1.4f;
        [SerializeField]
        private float _enlargeAnimTime = 0.4f;

        private Vector3 InitialPos => _initialPoint.position;
        private Vector3 SpawnPos => _itemsSpawnPos.position;
        private Quaternion SpawnRot => _itemsSpawnPos.rotation;
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
                    Quaternion rotation = SpawnRot * Quaternion.Euler(0, Random.Range(-RandomAngleOffset, RandomAngleOffset), 0);
                    Item newItem = ItemSystem.Instance.SpawnItem(InitialPos, rotation);
                    Vector3 rememberedSize = newItem.Collider.bounds.extents * 1.05f;

                    if (Collides(newItem, rememberedSize)) // передаем размер
                    {
                        Debug.Log($"<color=red>Spawned item collides, awaiting...</color>");
                        newItem.gameObject.SetActive(false);
                        await WaitForFreeSpaceAsync(newItem, rememberedSize, ctsToken);
                    }

                    newItem.transform.localScale = new Vector3(_itemInitialScale, _itemInitialScale, _itemInitialScale);
                    newItem.SetColor(randomColor);
                    newItem.gameObject.SetActive(true);

                    await MoveItemDownPipeAsync(newItem);

                    ItemSystem.Instance.InitItem(newItem, _lane);
                    _timer = SpawnInterval + Random.Range(-RandomSpawnOffset, RandomSpawnOffset);
                }

                _timer -= Time.deltaTime;

                await Awaitable.NextFrameAsync(ctsToken);
            }
        }

        private bool Collides(Item newItem, Vector3 rememberedSize)
        {
            int collisionsCount = Physics.OverlapBoxNonAlloc(SpawnPos, rememberedSize, _collisions, newItem.transform.rotation, _itemsMask);

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

        private Awaitable MoveItemDownPipeAsync(Item item)
        {
            Vector3 itemPos = item.transform.position;
            float distance = Vector3.Distance(itemPos, SpawnPos);
            Vector3 intermediatePos = itemPos + (SpawnPos - itemPos).normalized * (distance * _changeScaleMoveProgressRatio);

            _enlargeAnimScale = 0.5f;
            DOTween.Sequence()
                   .Append(item.transform.DOMove(intermediatePos, _pipeItemMoveDuration * _changeScaleMoveProgressRatio))
                   .Append(item.transform.DOMove(SpawnPos, _pipeItemMoveDuration * (1 - _changeScaleMoveProgressRatio)))
                   .Join(item.transform.DOScale(Vector3.one, _pipeItemMoveDuration * (1 - _changeScaleMoveProgressRatio))
                             .SetEase(_scaleToNormalSizeEase))
                   .AppendCallback(() => AudioManager.Instance.PlayItemSpawnSound(item.transform.position))
                   .Append(item.transform.DOPunchScale(_enlargeAnimScale * Vector3.one, _enlargeAnimTime, 1, 1))
                   .SetLink(item.gameObject);

            return Awaitable.WaitForSecondsAsync(_pipeItemMoveDuration);
        }

        private void OnDrawGizmos()
        {
            if (_checkSize != Vector3.zero)
            {
                Gizmos.DrawWireCube(SpawnPos, _checkSize);
            }
        }
    }
}
