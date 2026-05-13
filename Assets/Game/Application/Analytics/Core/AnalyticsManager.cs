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
#if YANDEX_GAMES_BUILD
            Debug.Log("YandexAnalyticsStrategy Initialized");
            _analyticsStrategy = new YandexAnalyticsStrategy();
#else
            _analyticsStrategy = new MockAnalyticsStrategy();
            Debug.Log("MockAnalyticsStrategy Initialized");
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
