namespace Game.Application.Ads
{
    public sealed class AnalyticsService : Singleton<AnalyticsService>
    {
        private IAnalyticMediator _analyticsMediator;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
#if UNITY_EDITOR
            _analyticsMediator = new MockAnalyticsMediator();
#elif UNITY_WWW
        _analyticsMediator = new YandexAnalyticsMediator();
#endif
        }

        public void SendEvent(string eventName)
        {
            _analyticsMediator.SendEvent(eventName);
        }

        public void SendEvent(string eventName, float value)
        {
        }
    }
}
