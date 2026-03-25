using UnityEngine;

namespace Game.Application.Ads.Observers
{
    public sealed class ItemConsumeAnalyticsObserver : MonoBehaviour
    {
        private void Start()
        {
            ScoreZone.OnRightColorItemConsumed += OnRightColorItemConsumed;
            ScoreZone.OnWrongColorItemConsumed += OnWrongColorItemConsumed;
        }

        private void OnRightColorItemConsumed(Item _)
        {
            AnalyticsManager.Instance.SendEvent(AnalyticsEvents.RIGHT_COLOR_ITEM_CONSUMED);
        }

        private void OnWrongColorItemConsumed(Item _)
        {
            AnalyticsManager.Instance.SendEvent(AnalyticsEvents.WRONG_COLOR_ITEM_CONSUMED);
        }

        private void OnDestroy()
        {
            ScoreZone.OnRightColorItemConsumed -= OnRightColorItemConsumed;
            ScoreZone.OnWrongColorItemConsumed -= OnWrongColorItemConsumed;
        }
    }
}
