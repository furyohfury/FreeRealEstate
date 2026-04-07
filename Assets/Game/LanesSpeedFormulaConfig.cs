using UnityEngine;

namespace Game
{
    public abstract class LanesSpeedFormulaConfig : ScriptableObject
    {
        public abstract LaneSpeedFormula GetLanesSpeedFormula();
    }
}