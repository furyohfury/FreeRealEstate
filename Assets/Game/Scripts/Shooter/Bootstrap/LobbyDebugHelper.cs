using Unity.Netcode;
using UnityEngine;

namespace Game
{
    public sealed class LobbyDebugHelper : MonoBehaviour
    {
        private void Awake()
        {
            NetworkManager.Singleton.OnClientConnectedCallback += SingletonOnOnClientConnectedCallback;
        }

        private void SingletonOnOnClientConnectedCallback(ulong obj)
        {
            Debug.Log($"connected client: {obj}");
        }

        private void OnDestroy()
        {
            NetworkManager manager = NetworkManager.Singleton;
            if (manager != null)
            {
                manager.OnClientConnectedCallback -= SingletonOnOnClientConnectedCallback;
            }
        }
    }
}
