using UnityEngine;

namespace Game.Application.Ads
{
    public class MockAnalyticsStrategy : IAnalyticStrategy
    {
        public void SendEvent(string eventName)
        {
            Debug.Log($"<color=green>Sent analytics event {eventName}</color>");
        }

        public void SendEvent(string eventName, float value)
        {
            Debug.Log($"<color=green>Sent analytics event {eventName} with value {value.ToString()}</color>");
        }
    }
}
