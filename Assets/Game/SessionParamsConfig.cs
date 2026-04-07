using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "SessionParams", menuName = "Game/SessionParams")]
    public sealed class SessionParamsConfig : ScriptableObject
    {
        public string Id;
        public float RewardForRightItemColor;
        public float PenaltyForWrongItemColor;
        public float PenaltyForCollision;
        public int LanesNumber;
        public LanesSpeedFormulaConfig lanesLaneSpeedFormulaConfig;
        public ItemSpawnIntervalFormulaConfig itemSpawnIntervalFormulaConfig;
        public GameColor[] GameColors;
        public float ContinueInitialHealthRatio = 0.5f;

        private SessionParams _sessionParams;

        private void OnEnable()
        {
            _sessionParams = null;
        }

        public SessionParams GetSessionParams()
        {
            _sessionParams ??= new SessionParams
                               {
                                   Id = Id,
                                   RewardForRightItemColor = RewardForRightItemColor,
                                   PenaltyForWrongItemColor = PenaltyForWrongItemColor,
                                   PenaltyForCollision = PenaltyForCollision,
                                   LanesNumber = LanesNumber,
                                   itemSpawnIntervalFormula = itemSpawnIntervalFormulaConfig.GetFormula(),
                                   GameColors = GameColors,
                                   ContinueInitialHealthRatio = ContinueInitialHealthRatio,
                                   lanesLaneSpeedFormula = lanesLaneSpeedFormulaConfig.GetLanesSpeedFormula()
                               };

            return _sessionParams;
        }
    }
}
