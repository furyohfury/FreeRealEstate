using DG.Tweening;
using Game.Infrastructure;
using TriInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public sealed class SessionRestarter : Singleton<SessionRestarter>
    {
        [Button]
        public void Restart()
        {
            DOTween.KillAll();
            // ItemSystem.Instance.ClearAll();
            // Health.Instance.CurrentHealth = Health.Instance.MaxHealth;
            // SessionLauncher.Instance.LaunchSession();
            Time.timeScale = 1;
            SceneSwitcher.Instance.ReloadScene();
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Keyboard.current.gKey.wasPressedThisFrame)
            {
                Restart();
            }
        }
#endif
    }
}
