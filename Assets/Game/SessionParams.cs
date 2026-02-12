using System;

namespace Game
{
    [Serializable]
    public struct SessionParams
    {
        public float RewardForRightItemColor;
        public float PenaltyForWrongItemColor;
        public float PenaltyForCollision;
    }
}
