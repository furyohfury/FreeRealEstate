using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DG.Tweening;
using UnityEngine;

namespace Game
{
    [SelectionBase]
    public sealed class ScoreZone : MonoBehaviour
    {
        public static event Action<Item> OnRightColorItemConsumed;
        public static event Action<Item> OnWrongColorItemConsumed;

        [field: SerializeField]
        public float ConsumeRadius { get; private set; } = 1f;
        [SerializeField]
        private Lane _lane;
        [SerializeField]
        private float _consumeDuration = 1f;
        [SerializeField]
        private Ease _consumeAnimEasing;
        [SerializeField]
        private MeshRenderer[] _renderers;

        [SerializeField]
        private Color _rightItemConsumedColor = Color.green;
        [SerializeField]
        private float _rightItemConsumedAnimDuration = 0.5f;
        [SerializeField]
        private Color _wrongItemConsumedColor = Color.red;
        [SerializeField]
        private float _wrongItemConsumedAnimDuration = 1f;

        private readonly HashSet<Item> _activeConsumingItems = new HashSet<Item>();
        private readonly HashSet<Tween> _activeTweens = new HashSet<Tween>();
        private Color[][] _initialColors;
        private CancellationTokenSource _cts;

        private void Awake()
        {
            _initialColors = new Color[_renderers.Length][];

            for (int i = 0, count = _renderers.Length; i < count; i++)
            {
                Material[] materials = _renderers[i].materials;
                _initialColors[i] = new Color[materials.Length];

                for (int j = 0, count1 = materials.Length; j < count1; j++)
                {
                    _initialColors[i][j] = materials[j].color;
                }
            }
        }

        public void StopAllConsumingItems()
        {
            foreach (Item item in _activeConsumingItems.ToArray())
            {
                ItemSystem.Instance.DestroyItem(item);
            }

            foreach (Tween tween in _activeTweens)
            {
                tween.Kill();
            }

            _activeConsumingItems.Clear();
            _activeTweens.Clear();
        }

        private void Update()
        {
            HashSet<Item> linkedItems = _lane.LinkedItems;
            List<Item> itemsToConsume = null;

            foreach (Item item in linkedItems)
            {
                if (!CanBeConsumed(item))
                    continue;

                Debug.Log("Scored");

                if (itemsToConsume == null)
                {
                    itemsToConsume = new List<Item>
                                     {
                                         item
                                     };
                }
                else
                {
                    itemsToConsume.Add(item);
                }
            }

            if (itemsToConsume != null)
            {
                ConsumeItems(itemsToConsume);
            }
        }

        private bool CanBeConsumed(Item item)
        {
            return IsInConsumeRadius(item) && item.IsPlayerControlled == false && _activeConsumingItems.Contains(item) == false;
        }

        public bool IsInConsumeRadius(Vector3 pos)
        {
            return (pos - transform.position).sqrMagnitude < ConsumeRadius * ConsumeRadius;
        }

        private bool IsInConsumeRadius(Item item)
        {
            return IsInConsumeRadius(item.GetPosition());
        }

        private void ConsumeItems(List<Item> itemsToConsume)
        {
            foreach (Item item in itemsToConsume)
            {
                _lane.RemoveItem(item);
                item.DisableCollision();
                _activeConsumingItems.Add(item);
                var sequence = DOTween.Sequence()
                                      .Append(item.transform.DOMove(transform.position, _consumeDuration).SetEase(_consumeAnimEasing))
                                      .Join(item.ChangeSize(0, _consumeDuration, _consumeAnimEasing))
                                      .AppendCallback(() =>
                                      {
                                          OnItemConsumedCallback(item);
                                      })
                                      .SetLink(item.gameObject);

                _activeTweens.Add(sequence);
            }
        }

        private void OnItemConsumedCallback(Item item)
        {
            _activeConsumingItems.Remove(item);

            if (IsItemSameColorWithLane(item))
            {
                HealthController.Instance.RewardForRightColor();
                VFXManager.Instance.SpawnRightColorItemConsumedVFX(transform.position);
                _cts?.Cancel();
                _cts = new CancellationTokenSource();
                LaunchColorAnim(_rightItemConsumedColor, _rightItemConsumedAnimDuration, _cts.Token);
                OnRightColorItemConsumed?.Invoke(item);
            }
            else
            {
                HealthController.Instance.PenalizeForWrongColor();
                VFXManager.Instance.SpawnWrongColorItemConsumedVFX(transform.position);
                _cts?.Cancel();
                _cts = new CancellationTokenSource();
                LaunchColorAnim(_wrongItemConsumedColor, _wrongItemConsumedAnimDuration, _cts.Token);
                OnWrongColorItemConsumed?.Invoke(item);
            }

            ItemSystem.Instance.DestroyItem(item);
        }

        private bool IsItemSameColorWithLane(Item item)
        {
            return item.GameColor == _lane.GameColor;
        }

        private async Awaitable LaunchColorAnim(Color color, float duration, CancellationToken token)
        {
            for (int i = 0, count = _renderers.Length; i < count; i++)
            {
                Material[] materials = _renderers[i].materials;

                for (int j = 0, count1 = materials.Length; j < count1; j++)
                {
                    materials[j].color = color;
                }
            }

            await Awaitable.WaitForSecondsAsync(duration, token);

            RestoreInitialColors();
        }

        private void RestoreInitialColors()
        {
            for (int i = 0, count = _renderers.Length; i < count; i++)
            {
                Material[] materials = _renderers[i].materials;

                for (int j = 0, count1 = materials.Length; j < count1; j++)
                {
                    materials[j].color = _initialColors[i][j];
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, ConsumeRadius);
        }
    }
}
