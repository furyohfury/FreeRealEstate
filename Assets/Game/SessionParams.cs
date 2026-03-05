using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "SessionParams", menuName = "Game/SessionParams")]
    public sealed class SessionParams : ScriptableObject
    {
        public string Id;
        public float RewardForRightItemColor;
        public float PenaltyForWrongItemColor;
        public float PenaltyForCollision;
        public int LanesNumber;
        public LanesSpeedFormula LanesSpeedFormula;
        public ItemSpawnIntervalFormula ItemSpawnIntervalFormula;
        public GameColor[] GameColors;

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
