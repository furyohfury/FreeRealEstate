using Unity.Netcode;
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
        [Inject]
        LobbySystem _lobby;
        [Inject]
        private PlayerFactory _playerFactory;

        private void OnEnable()
        {
            NetworkManager.Singleton.OnClientConnectedCallback += SingletonOnOnClientConnectedCallback;
        }

        private void SingletonOnOnClientConnectedCallback(ulong obj)
        {
            if (NetworkManager.Singleton.IsHost == false || _lobby.SessionInfo != null)
            {
                return;
            }

            Debug.Log($"<color=yellow>EditorHostStarter spawning player prefab for client {obj}</color>", this);
            _playerFactory.SpawnPlayer(obj);
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

        [Rpc(SendTo.Server)]
        private void SpawnClientPlayerObjRpc(ulong localClientId)
        {
            _playerFactory.SpawnPlayer(localClientId);
        }

        private void OnDisable()
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= SingletonOnOnClientConnectedCallback;
        }
    }
}
