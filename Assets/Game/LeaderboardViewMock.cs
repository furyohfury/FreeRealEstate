using UnityEngine;

namespace Game
{
    public sealed class LeaderboardViewMock : LeaderboardView
    {
        [SerializeField]
        private GameObject _leaderboard;

        public override void UpdateLeaderboard()
        {
            _leaderboard.SetActive(true);
        }
    }
}
