using UnityEngine;
using YG;

namespace Game.Application.Ads
{
    public sealed class AnalyticsManager : Singleton<AnalyticsManager>
    {
        private IAnalyticStrategy _analyticsStrategy;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
#if UNITY_EDITOR
            _analyticsStrategy = new MockAnalyticsStrategy();
            Debug.Log("MockAnalyticsStrategy Initialized");
#elif UNITY_WEBGL
            Debug.Log("YandexAnalyticsStrategy Initialized");
            _analyticsStrategy = new YandexAnalyticsStrategy();
#endif
        }

        public void SendEvent(string eventName)
        {
            _analyticsStrategy.SendEvent(eventName);
        }

        public void SendEvent(string eventName, float value)
        {
        }
    }
}
