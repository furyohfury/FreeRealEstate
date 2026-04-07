using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "LinearLanesSpeedFormula", menuName = "Game/LinearLanesSpeedFormula")]
    public class LinearLanesSpeedFormula : LanesSpeedFormula
    {
        public float _baseSpeed;
        public AnimationCurve _curve;
        public float _timeCoefficient;
        public float _multiplier;

        public override float GetLanesSpeed(float sessionTime)
        {
            return _baseSpeed + _curve.Evaluate(sessionTime * _timeCoefficient) *  _multiplier;
        }
    }
}
