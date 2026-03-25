using UnityEngine;
using YG;

namespace Game.Application.Leaderboard
{
    public class YGLeaderboardStrategy : LeaderboardStrategy
    {
        [SerializeField]
        private LeaderboardYG _leaderboard;
        private const string LEADERBOARD_NAME = "MaxTimeLeaderboard";

        public override void SendResult(float result)
        {
            YG2.SetLBTimeConvert(LEADERBOARD_NAME, result);
        }

        public override void ShowLeaderboard()
        {
            Debug.Log("Show mock Leaderboard");
            _leaderboard.gameObject.SetActive(true);
        }

        public override void HideLeaderboard()
        {
            Debug.Log("hide mock Leaderboard");
            _leaderboard.gameObject.SetActive(false);
        }
    }
}
