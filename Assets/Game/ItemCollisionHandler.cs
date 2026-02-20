using System;
using DG.Tweening;
using UnityEngine;

namespace Game
{
    public sealed class ItemCollisionHandler : MonoBehaviour
    {
        public event Action<Item> OnDestroyItem;

        [SerializeField]
        private float _knockDistance;
        [SerializeField]
        private float _speed;
        [SerializeField]
        private Ease _moveAnimEase = Ease.OutCirc;

        public void SubscribeToCollisionEvents(Item item)
        {
            item.OnKnocked += ItemOnOnKnocked;
        }

        private void ItemOnOnKnocked(Item item, Item knockedItem)
        {
            knockedItem.DisableCollision();
            HealthController.Instance.PenalizeForCollision();

            Vector3 swipedItemPos = item.GetPosition();
            Vector3 knockedItemPos = knockedItem.GetPosition();
            Vector3 direction = knockedItemPos - swipedItemPos;
            direction.Normalize();
            direction *= _knockDistance;
            var duration = direction.magnitude / _speed;
            var targetPos = knockedItemPos + direction;

            // TODO VFX

            DOTween.Sequence()
                   .Append(knockedItem.transform.DOMove(targetPos, duration).SetEase(_moveAnimEase))
                   .Append(ItemAnimationSystem.Instance.ScaleOnKnockAnim(knockedItem.transform))
                   .AppendCallback(() =>
                   {
                       // TODO VFX
                       OnDestroyItem?.Invoke(knockedItem);
                   });
        }

        public void UnsubscribeToCollisionEvents(Item item)
        {
            item.OnKnocked -= ItemOnOnKnocked;
        }
    }
}
