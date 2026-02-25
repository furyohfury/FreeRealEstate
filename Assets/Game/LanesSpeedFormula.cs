using UnityEngine;

namespace Game
{
    public abstract class LanesSpeedFormula : ScriptableObject
    {
        public abstract float GetLanesSpeed(float sessionTime);
    }
}