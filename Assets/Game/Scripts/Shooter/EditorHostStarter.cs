using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.Shooter
{
    public sealed class EditorHostStarter : MonoBehaviour
    {
        [SerializeField]
        private bool _enabled = true;

        private void Start()
        {
            // NetworkManager.Singleton.StartHost();
        }

        private void Update()
        {
            if (_enabled == false)
            {
                return;
            }

            if (Keyboard.current.hKey.wasPressedThisFrame)
            {
                NetworkManager.Singleton.StartHost();
            }
            else if (Keyboard.current.cKey.wasPressedThisFrame)
            {
                NetworkManager.Singleton.StartClient();
            }
        }
    }
}
