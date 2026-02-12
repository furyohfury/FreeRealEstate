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
        private Ease _ease = Ease.OutCirc;

        public void SubscribeToCollisionEvents(Item item)
        {
            item.OnCollided += ItemOnOnCollided;
        }

        private void ItemOnOnCollided(Item item, Item knockedItem)
        {
            knockedItem.DisableCollision();
            Health.Instance.CurrentHealth -= GameParams.Instance.Params.PenaltyForCollision;
            
            Vector3 swipedItemPos = item.GetPosition();
            Vector3 knockedItemPos = knockedItem.GetPosition();
            Vector3 direction = knockedItemPos - swipedItemPos;
            direction.Normalize();
            direction *= _knockDistance;
            var duration = direction.magnitude / _speed;
            var targetPos = knockedItemPos + direction;

            // TODO VFX

            DOTween.Sequence()
                   .Append(knockedItem.transform.DOMove(targetPos, duration).SetEase(_ease))
                   .Append(knockedItem.ChangeSize(0, duration, _ease))
                   .AppendCallback(() =>
                   {
                       // TODO VFX
                       OnDestroyItem?.Invoke(item);
                   });
        }

        public void UnsubscribeToCollisionEvents(Item item)
        {
            item.OnCollided -= ItemOnOnCollided;
        }
    }
}
