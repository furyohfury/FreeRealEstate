using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "ItemSpawnIntervalFormulaConst", menuName = "Game/ItemSpawnIntervalFormulaConst")]
    public class ItemSpawnIntervalFormulaConst : ItemSpawnIntervalFormula
    {
        [SerializeField]
        private float _interval;
        [SerializeField]
        private float _randomSpawnOffset;

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