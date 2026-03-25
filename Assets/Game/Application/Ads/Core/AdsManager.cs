using UnityEngine;

namespace Game.Application.Ads
{
    public sealed class AdsManager : Singleton<AdsManager>
    {
        private IAdsStrategy _adsStrategy;
        
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
#if UNITY_EDITOR
            _adsStrategy = new MockAdsStrategy();
            Debug.Log("MockAdsStrategy Initialized");
#elif UNITY_WEBGL
            Debug.Log("YGAdsStrategy Initialized");
            _adsStrategy = new YGAdsStrategy();
#endif
        }

        public Awaitable ShowRewardAd(string id)
        {
            return _adsStrategy.ShowRewardAd(id);
        }
    }
}
