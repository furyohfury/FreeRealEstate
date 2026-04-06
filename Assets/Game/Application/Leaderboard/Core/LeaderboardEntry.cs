using System;
using UnityEngine;

namespace Game.Application.Leaderboard
{
    public struct LeaderboardEntry
    {
        public int Position;
        public Sprite ProfileImage;
        public string Nickname;
        public TimeSpan Score;
    }
}
