using UnityEngine;

namespace Game
{
    public abstract class ItemSpawnIntervalFormula : ScriptableObject
    {
        public abstract float GetInterval(float sessionTime);
    }
}