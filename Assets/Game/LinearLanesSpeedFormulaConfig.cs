using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "LinearLanesSpeedFormula", menuName = "Game/LinearLanesSpeedFormula")]
    public class LinearLanesSpeedFormulaConfig : LanesSpeedFormulaConfig
    {
        public float baseSpeed = 3.5f;
        public float timeCoefficient = 1f;
        private LaneSpeedFormulaLinear _laneSpeedFormulaLinear;

        private void OnEnable()
        {
            _laneSpeedFormulaLinear = null;
        }

        public override LaneSpeedFormula GetLanesSpeedFormula()
        {
            _laneSpeedFormulaLinear ??= new LaneSpeedFormulaLinear(baseSpeed, timeCoefficient);

            return _laneSpeedFormulaLinear;
        }
    }
}
