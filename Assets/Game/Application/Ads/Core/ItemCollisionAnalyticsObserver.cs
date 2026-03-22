using UnityEngine;

namespace Game.Application.Ads
{
    public sealed class ItemCollisionAnalyticsObserver : MonoBehaviour
    {
        private void Start()
        {
            ItemCollisionHandler.Instance.OnDestroyItem += OnDestroyItem;
        }

        private void OnDestroyItem(Item _)
        {
            AnalyticsService.Instance.SendEvent(AnalyticsEvents.ITEM_COLLIDED);
        }

        private void OnDestroy()
        {
            ItemCollisionHandler.Instance.OnDestroyItem -= OnDestroyItem;
        }
    }
}
