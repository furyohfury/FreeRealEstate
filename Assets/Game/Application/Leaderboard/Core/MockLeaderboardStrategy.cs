using UnityEngine;

namespace Game.Application.Leaderboard
{
    public class MockLeaderboardStrategy : LeaderboardStrategy
    {
        // private const string LEADERBOARD_NAME = "MaxTimeLeaderboard";

        public override LeaderboardEntry[] GetEntries(string leaderboardId)
        {
            return new LeaderboardEntry[]
                   {
                   };
        }

        public override void SendResult(float result)
        {
            Debug.Log("SendResult Leaderboard" + result);
        }
    }
}
