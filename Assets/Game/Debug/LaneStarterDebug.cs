using System;
using TriInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Game
{
    public sealed class LaneStarterDebug : MonoBehaviour
    {
        [Button]
        public void StartSpawn()
        {
            SessionLauncher.Instance.LaunchSession();
        }

        private void Update()
        {
            if (Keyboard.current.lKey.wasPressedThisFrame)
            {
                StartSpawn();
            }
        }
    }
}
