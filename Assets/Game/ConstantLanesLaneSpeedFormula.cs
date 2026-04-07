namespace Game
{
    public class ConstantLanesLaneSpeedFormula : LaneSpeedFormula
    {
        public float baseSpeed = 4;

        public ConstantLanesLaneSpeedFormula(float baseSpeed)
        {
            this.baseSpeed = baseSpeed;
        }

        public override float GetLanesSpeed(float sessionTime)
        {
            return baseSpeed;
        }
    }
}
