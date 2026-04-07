using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "ConstantLanesSpeedFormula", menuName = "Game/ConstantLanesSpeedFormula")]
    public class ConstantLanesSpeedFormula : LanesSpeedFormula
    {
        public float _baseSpeed;

        public override float GetLanesSpeed(float sessionTime)
        {
            return _baseSpeed;
        }
    }
}
