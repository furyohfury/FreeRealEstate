using System;
using UnityEngine;

namespace Game.Application.Ads
{
    public sealed class AdsManager : Singleton<AdsManager>
    {
        [SerializeField]
        private PlatformMode _platformMode = PlatformMode.Mock;
        private IAdsStrategy _adsStrategy;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);

#if UNITY_EDITOR
            switch (_platformMode)
            {
                case PlatformMode.Mock:
                    _adsStrategy = new MockAdsStrategy();
                    break;
                case PlatformMode.YG:
                    _adsStrategy = new YGAdsStrategy();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
#elif YANDEX_GAMES_BUILD
            _adsStrategy = new YGAdsStrategy();
#endif
            Debug.Log($"Ads strategy of type {_adsStrategy.GetType()} Initialized");
        }

        public Awaitable ShowRewardAd(string id)
        {
            return _adsStrategy.ShowRewardAd(id);
        }
    }
}
