using Game.Application.Ads;
using TriInspector;
using UnityEngine;

namespace Game.Application.Leaderboard
{
    public class LeaderboardManager : Singleton<LeaderboardManager>
    {
        [SerializeField]
        private LeaderboardStrategy _leaderboardStrategy;
        
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        public void SendResult(float result)
        {
            _leaderboardStrategy.SendResult(result);
        }
        
        [Button]
        public void ShowLeaderboard()
        {
            _leaderboardStrategy.ShowLeaderboard();
        }

        [Button]
        public void HideLeaderboard()
        {
            _leaderboardStrategy.HideLeaderboard();
        }
    }
}
