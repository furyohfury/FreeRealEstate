using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class SessionParams
    {
        public string Id;
        public float RewardForRightItemColor;
        public float PenaltyForWrongItemColor;
        public float PenaltyForCollision;
        public int LanesNumber;
        public LaneSpeedFormula lanesLaneSpeedFormula;
        public ItemSpawnIntervalFormula itemSpawnIntervalFormula;
        public GameColor[] GameColors;
        public float ContinueInitialHealthRatio = 0.5f;

        private void OnValidate()
        {
            if (GameColors.Length != LanesNumber)
            {
                var currentColors = GameColors;
                GameColors = new GameColor[LanesNumber];
                int length = Mathf.Min(LanesNumber, currentColors.Length);

                for (int i = 0; i < length; i++)
                {
                    GameColors[i] = currentColors[i];
                }
            }
        }
    }
}
