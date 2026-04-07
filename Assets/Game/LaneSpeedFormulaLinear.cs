namespace Game
{
    public class LaneSpeedFormulaLinear : LaneSpeedFormula
    {
        public float baseSpeed = 3.5f;
        public float timeCoefficient = 1f;

        public LaneSpeedFormulaLinear(float baseSpeed, float timeCoefficient)
        {
            this.baseSpeed = baseSpeed;
            this.timeCoefficient = timeCoefficient;
        }

        public override float GetLanesSpeed(float sessionTime)
        {
            return baseSpeed + sessionTime * timeCoefficient;
        }
    }
}
