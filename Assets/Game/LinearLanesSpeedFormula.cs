using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "LinearLanesSpeedFormula", menuName = "Game/LinearLanesSpeedFormula")]
    public class LinearLanesSpeedFormula : LanesSpeedFormula
    {
        [SerializeField]
        private float _baseSpeed;
        [SerializeField]
        private AnimationCurve _curve;
        [SerializeField]
        private float _timeCoefficient;
        [SerializeField]
        private float _multiplier;

        public override float GetLanesSpeed(float sessionTime)
        {
            return _baseSpeed + _curve.Evaluate(sessionTime * _timeCoefficient) *  _multiplier;
        }
    }
}
