using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "ItemSpawnIntervalFormulaConst", menuName = "Game/ItemSpawnIntervalFormulaConst")]
    public class ItemSpawnIntervalFormulaConst : ItemSpawnIntervalFormula
    {
        public float _interval;
        public float _randomSpawnOffset;

        public override float GetInterval()
        {
            return _interval;
        }

        public override float GetRandomSpawnOffset()
        {
            return _randomSpawnOffset;
        }
    }
}