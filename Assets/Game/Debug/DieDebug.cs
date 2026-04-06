using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class DieDebug : MonoBehaviour
    {
        private void Update()
        {
            if (Keyboard.current.kKey.wasPressedThisFrame)
            {
                Health.Instance.CurrentHealth = 0;
            }
        }
    }
}
