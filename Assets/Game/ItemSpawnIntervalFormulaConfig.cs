using UnityEngine;

namespace Game
{
    public abstract class ItemSpawnIntervalFormulaConfig : ScriptableObject
    {
        public abstract ItemSpawnIntervalFormula GetFormula();
    }
}