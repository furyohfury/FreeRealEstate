using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Game
{
    [SelectionBase]
    public sealed class ScoreZone : MonoBehaviour
    {
        [field: SerializeField]
        public float ConsumeRadius { get; private set; } = 1f;
        [SerializeField]
        private Lane _lane;
        [SerializeField]
        private float _consumeDuration = 1f;
        [SerializeField]
        private Ease _consumeAnimEasing;

        private readonly HashSet<Item> _activeConsumingItems = new HashSet<Item>();

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
                DOTween.Sequence()
                       .Append(item.transform.DOMove(transform.position, _consumeDuration).SetEase(_consumeAnimEasing))
                       .Join(item.ChangeSize(0, ConsumeRadius, _consumeAnimEasing))
                       .AppendCallback(() =>
                       {
                           OnItemConsumed(item);
                       });
            }
        }

        private void OnItemConsumed(Item item)
        {
            _activeConsumingItems.Remove(item);

            if (IsItemSameColorWithLane(item))
            {
                HealthController.Instance.RewardForRightColor();
                // TODO vfx
            }
            else
            {
                HealthController.Instance.PenalizeForWrongColor();
                // TODO vfx
            }

            ItemSystem.Instance.DestroyItem(item);
        }

        private bool IsItemSameColorWithLane(Item item)
        {
            return item.GameColor == _lane.GameColor;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, ConsumeRadius);
        }
    }
}
