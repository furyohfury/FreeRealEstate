using UnityEngine;

namespace Game.Application.Leaderboard
{
    public abstract class LeaderboardStrategy : MonoBehaviour
    {
        public abstract void ShowLeaderboard();
        public abstract void HideLeaderboard();
        public abstract void SendResult(float result);
    }
}