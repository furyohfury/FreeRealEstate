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
#if YANDEX_GAMES_BUILD
            _leaderboardStrategy = _ygLeaderboardStrategy;
#else
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
