using UnityEngine;

namespace Game.Application.Leaderboard
{
    public abstract class LeaderboardStrategy : MonoBehaviour
    {
        public abstract LeaderboardEntry[] GetEntries(string leaderboardId);
        public abstract void SendResult(float result);
    }
}