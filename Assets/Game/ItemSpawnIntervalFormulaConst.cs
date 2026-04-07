namespace Game
{
    public class ItemSpawnIntervalFormulaConst : ItemSpawnIntervalFormula
    {
        public float interval;
        public float randomSpawnOffset;

        public ItemSpawnIntervalFormulaConst(float interval, float randomSpawnOffset)
        {
            this.interval = interval;
            this.randomSpawnOffset = randomSpawnOffset;
        }

        public override float GetInterval()
        {
            return interval;
        }

        public override float GetRandomSpawnOffset()
        {
            return randomSpawnOffset;
        }
    }
}
