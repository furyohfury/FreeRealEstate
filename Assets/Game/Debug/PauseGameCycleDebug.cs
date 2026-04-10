using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public sealed class PauseGameCycleDebug : MonoBehaviour
    {
        private void Update()
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                GameCycleStateSwitcher.Instance.PauseGame();
            }
            if (Keyboard.current.cKey.wasPressedThisFrame)
            {
                GameCycleStateSwitcher.Instance.ResumeGame();
            }
        }
    }
}
