using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Game
{
    public sealed class ScoreZone : MonoBehaviour
    {
        [SerializeField]
        private ItemSystem _itemSystem;
        [SerializeField]
        private Lane _lane;
        [SerializeField]
        private Health _health;
        [SerializeField]
        private GameParams _gameParams;
        [SerializeField]
        private float _consumeRadius = 1f;
        [SerializeField]
        private float _consumeDuration = 1f;
        [SerializeField]
        private Ease _consumeAnimEasing;

        private readonly HashSet<Item> _activeConsumingItems = new HashSet<Item>();

        private void Update()
        {
            HashSet<Item> linkedItems = _lane.LinkedItems;

            foreach (Item item in linkedItems) // TODO modified collection
            {
                if (IsInConsumeRadius(item)
                    && _activeConsumingItems.Contains(item) == false)
                {
                    Debug.Log("Scored");

                    _lane.RemoveItem(item);
                    _activeConsumingItems.Add(item);
                    DOTween.Sequence()
                           .Append(item.MoveTo(transform.position, _consumeDuration, _consumeAnimEasing))
                           .Join(item.ChangeSize(0, _consumeRadius, _consumeAnimEasing))
                           .AppendCallback(() =>
                           {
                               OnItemConsumed(item);
                           });
                }
            }
        }

        private void OnItemConsumed(Item item)
        {
            _activeConsumingItems.Remove(item);
            _itemSystem.DestroyItem(item);

            if (item.Color == _lane.Color)
            {
                _health.CurrentHealth += _gameParams.Params.RewardForRightItemColor;
                // vfx
            }
            else
            {
                _health.CurrentHealth -= _gameParams.Params.PenaltyForWrongItemColor;
                // vfx
            }
        }

        private bool IsInConsumeRadius(Item item)
        {
            return (item.GetPosition() - transform.position).sqrMagnitude < _consumeRadius * _consumeRadius;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _consumeRadius);
        }
    }
}
