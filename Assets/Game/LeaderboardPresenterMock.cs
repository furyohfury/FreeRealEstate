using UnityEngine;

namespace Game
{
    public sealed class LeaderboardPresenterMock : LeaderboardPresenter
    {
        [SerializeField]
        private GameObject _leaderboard;

        public override void ShowLeaderboard()
        {
            _leaderboard.SetActive(true);
        }

        public override void HideLeaderboard()
        {
            _leaderboard.SetActive(false);
        }
    }
}
