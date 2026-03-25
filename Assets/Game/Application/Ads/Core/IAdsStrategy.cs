using UnityEngine;

namespace Game.Application.Ads
{
    public interface IAdsStrategy
    {
        Awaitable ShowRewardAd(string id);
    }
}
