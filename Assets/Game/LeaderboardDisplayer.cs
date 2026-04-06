using System;
using TriInspector;
using UnityEngine;

namespace Game
{
    public sealed class LeaderboardDisplayer : Singleton<LeaderboardDisplayer>
    {
        [SerializeField]
        private Mode _mode;
        [SerializeField]
        private LeaderboardPresenterMock _leaderboardPresenterMock;
        [SerializeField]
        private LeaderboardPresenterYG _leaderboardPresenterYG;
        private LeaderboardPresenter _leaderboardPresenter;

        private void Start()
        {
            switch (_mode)
            {
                case Mode.Mock:
                    _leaderboardPresenter = _leaderboardPresenterMock;
                    _leaderboardPresenterYG.enabled = false;
                    break;
                case Mode.YG:
                    _leaderboardPresenter = _leaderboardPresenterYG;
                    _leaderboardPresenterMock.enabled = false;
                    break;
            }
        }

        [Button]
        public void ShowLeaderboard()
        {
            _leaderboardPresenter.ShowLeaderboard();
        }

        [Button]
        public void HideLeaderboard()
        {
            _leaderboardPresenter.HideLeaderboard();
        }

        [Flags]
        private enum Mode
        {
            Mock,
            YG
        }
    }
}
