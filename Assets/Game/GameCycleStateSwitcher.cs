using TriInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class GameCycleStateSwitcher : Singleton<GameCycleStateSwitcher>
    {
        [Button]
        public void PauseGame()
        {
            GameLoop.Instance.Pause();
            Time.timeScale = 0;
            InputSystem.Instance.Disable();
        }

        [Button]
        public void ResumeGame()
        {
            GameLoop.Instance.Resume();
            Time.timeScale = 1;
            InputSystem.Instance.Enable();
        }

        #if UNITY_EDITOR
        private void Update()
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                PauseGame();
            }
            if (Keyboard.current.cKey.wasPressedThisFrame)
            {
                ResumeGame();
            }
        }
        #endif
    }
}
