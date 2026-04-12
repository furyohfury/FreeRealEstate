using UnityEngine;

namespace Game.Application.Leaderboard
{
    public class LeaderboardManager : Singleton<LeaderboardManager>
    {
        [SerializeField]
        private MockLeaderboardStrategy _mockLeaderboardStrategy;
        [SerializeField]
        private YGLeaderboardStrategy _ygLeaderboardStrategy;
        private LeaderboardStrategy _leaderboardStrategy;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
#if UNITY_WEBGL
            _leaderboardStrategy = _ygLeaderboardStrategy;
#elif UNITY_EDITOR
            _leaderboardStrategy = _mockLeaderboardStrategy;
#endif
        }

        public async Awaitable SendResult(float result)
        {
            await _leaderboardStrategy.SendResult(result);
        }

        public LeaderboardEntry[] GetEntries(string leaderboardId)
        {
            return _leaderboardStrategy.GetEntries(leaderboardId);
        }
    }
}
