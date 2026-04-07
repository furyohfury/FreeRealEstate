using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "ItemSpawnIntervalFormulaConst", menuName = "Game/ItemSpawnIntervalFormulaConst")]
    public class ItemSpawnIntervalFormulaConfigConst : ItemSpawnIntervalFormulaConfig
    {
        public float interval = 3.3f;
        public float randomSpawnOffset = 1.5f;
        private ItemSpawnIntervalFormulaConst _formulaConst;

        private void OnEnable()
        {
            _formulaConst = null;
        }

        public override ItemSpawnIntervalFormula GetFormula()
        {
            _formulaConst ??= new ItemSpawnIntervalFormulaConst(interval, randomSpawnOffset);

            return _formulaConst;
        }
    }
}
