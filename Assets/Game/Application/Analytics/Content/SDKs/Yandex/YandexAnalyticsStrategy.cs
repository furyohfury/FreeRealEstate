using UnityEngine;
using YG;

namespace Game.Application.Ads
{
    public sealed class YandexAnalyticsStrategy : IAnalyticStrategy
    {
        public void SendEvent(string eventName)
        {
            Debug.Log($"<color=green>Sent analytics event {eventName}</color>");
            YG2.MetricaSend(eventName);
        }

        public void SendEvent(string eventName, float value)
        {
            Debug.Log($"<color=green>Sent analytics event {eventName} with value {value.ToString()}</color>");
        }
    }
}
