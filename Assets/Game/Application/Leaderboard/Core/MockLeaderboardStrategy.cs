using UnityEngine;

namespace Game.Application.Leaderboard
{
    public class MockLeaderboardStrategy : LeaderboardStrategy
    {
        [SerializeField]
        private GameObject _leaderboard;

        private const string LEADERBOARD_NAME = "MaxTimeLeaderboard";

        public override void SendResult(float result)
        {
            Debug.Log("SendResult Leaderboard" + result);
        }

        public override void ShowLeaderboard()
        {
            Debug.Log("Show mock Leaderboard");

            if (_leaderboard != null)
                _leaderboard.SetActive(true);
        }

        public override void HideLeaderboard()
        {
            Debug.Log("hide mock Leaderboard");

            if (_leaderboard != null)
                _leaderboard.SetActive(false);
        }
    }
}
