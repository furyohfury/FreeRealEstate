using UnityEngine;
using YG;

namespace Game.Application.Ads
{
    public class YGAdsStrategy : IAdsStrategy
    {
        private AwaitableCompletionSource _acs;

        public Awaitable ShowRewardAd(string id)
        {
            _acs = new AwaitableCompletionSource();
            YG2.RewardedAdvShow(id, OnAdFinished);

            return _acs.Awaitable;
        }

        private async void OnAdFinished()
        {
            await Awaitable.NextFrameAsync();

            _acs.TrySetResult();
        }
    }
}
