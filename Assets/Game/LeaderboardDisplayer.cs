using System;
using UnityEngine;

namespace Game
{
    public sealed class LeaderboardDisplayer : Singleton<LeaderboardDisplayer>
    {
        [SerializeField]
        private Mode _mode;
        [SerializeField]
        private LeaderboardPresenterMock _leaderboardPresenterMock;
        private LeaderboardPresenter _leaderboardPresenter;

        private void Start()
        {
            switch (_mode)
            {
                case Mode.Mock:
                    _leaderboardPresenter = _leaderboardPresenterMock;
                    break;
            }
        }

        public void ShowLeaderboard()
        {
            _leaderboardPresenter.ShowLeaderboard();
        }

        public void HideLeaderboard()
        {
            _leaderboardPresenter.HideLeaderboard();
        }

        [Flags]
        private enum Mode
        {
            Mock
        }
    }
}
