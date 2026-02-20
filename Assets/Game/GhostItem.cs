using System;
using UnityEngine;

namespace Game
{
    public sealed class GhostItem : MonoBehaviour
    {
        public event Action<GhostItem> OnReachedScoreZone; 
        
        public void ReachedScoreZone()
        {
            OnReachedScoreZone?.Invoke(this);
            Destroy();
            // TODO VFX dissolve mb and destroy
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
