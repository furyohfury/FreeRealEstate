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

        public override LeaderboardEntry[] GetEntries(string leaderboardId)
        {
            return null;
        }
    }
}
