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

        public void SendResult(float result)
        {
            _leaderboardStrategy.SendResult(result);
        }

        public LeaderboardEntry[] GetEntries(string leaderboardId)
        {
            return _leaderboardStrategy.GetEntries(leaderboardId);
        }
    }
}
