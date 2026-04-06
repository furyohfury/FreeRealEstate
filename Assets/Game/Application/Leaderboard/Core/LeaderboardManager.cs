using UnityEngine;

namespace Game.Application.Leaderboard
{
    public class LeaderboardManager : Singleton<LeaderboardManager>
    {
        [SerializeField]
        private MockLeaderboardStrategy _mockLeaderboardStrategy;
        private LeaderboardStrategy _leaderboardStrategy;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
            _leaderboardStrategy = _mockLeaderboardStrategy;
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
