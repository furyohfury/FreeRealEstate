using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "ItemSpawnIntervalFormulaConst", menuName = "Game/ItemSpawnIntervalFormulaConst")]
    public class ItemSpawnIntervalFormulaConst : ItemSpawnIntervalFormula
    {
        [SerializeField]
        private float _interval;
        
        public override float GetInterval(float sessionTime)
        {
            return _interval;
        }
    }
}