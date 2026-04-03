using System;
using UnityEngine;

namespace Game
{
    public sealed class DestroyEventTracker : MonoBehaviour
    {
        public event Action<DestroyEventTracker> OnDestroyed;

        private void OnDestroy()
        {
            OnDestroyed?.Invoke(this);
        }
    }
}
