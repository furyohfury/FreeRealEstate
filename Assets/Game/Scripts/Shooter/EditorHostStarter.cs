using System;
using Unity.Netcode;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.Scripts.Shooter
{
    public sealed class EditorHostStarter : MonoBehaviour
    {
        [SerializeField]
        private bool _enabled = true;
        [Inject]
        private SessionSystem _sessionSystem;

        private void Start()
        {
            // if (AuthenticationService.Instance.IsAuthorized == false)
            // {
            //     AuthenticationService.Instance.SignInAnonymouslyAsync();
            // }
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
                _sessionSystem.SpawnPlayerObject(NetworkManager.Singleton.LocalClientId, "host");
            }
            else if (Keyboard.current.cKey.wasPressedThisFrame)
            {
                NetworkManager.Singleton.StartClient();
                SpawnClientPlayerObjRpc(NetworkManager.Singleton.LocalClientId);
            }
        }

        [Rpc(SendTo.Server)]
        private void SpawnClientPlayerObjRpc(ulong localClientId)
        {
            _sessionSystem.SpawnPlayerObject(localClientId, "client");
        }
    }
}
