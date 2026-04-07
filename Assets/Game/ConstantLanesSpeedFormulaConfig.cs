using System;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "ConstantLanesSpeedFormula", menuName = "Game/ConstantLanesSpeedFormula")]
    public class ConstantLanesSpeedFormulaConfig : LanesSpeedFormulaConfig
    {
        public float baseSpeed = 4;
        private ConstantLanesLaneSpeedFormula _constantLanesLaneSpeedFormula;

        private void OnEnable()
        {
            _constantLanesLaneSpeedFormula = null;
        }

        public override LaneSpeedFormula GetLanesSpeedFormula()
        {
            _constantLanesLaneSpeedFormula ??= new ConstantLanesLaneSpeedFormula(baseSpeed);

            return _constantLanesLaneSpeedFormula;
        }
    }
}
