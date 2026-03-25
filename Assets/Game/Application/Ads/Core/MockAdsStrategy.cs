using UnityEngine;

namespace Game.Application.Ads
{
    public class MockAdsStrategy : IAdsStrategy
    {
        public Awaitable ShowRewardAd(string id)
        {
            Debug.Log("Showing reward ad: " + id);

            return Awaitable.WaitForSecondsAsync(3);
        }
    }
}
