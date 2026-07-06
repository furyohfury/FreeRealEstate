using Cysharp.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public sealed class LobbyGameplayLauncher
    {
        private readonly NetworkManager _networkManager;

        public LobbyGameplayLauncher(NetworkManager networkManager)
        {
            _networkManager = networkManager;
        }

        public async UniTask LaunchGame(SessionInfo sessionInfo)
        {
            Debug.Log("Launching lobby...");
            Debug.Log("3");
            await UniTask.Delay(1000);
            Debug.Log("2");
            await UniTask.Delay(1000);
            Debug.Log("1");
            await UniTask.Delay(1000);
            Debug.Log("Switching to gameplay scene...");

            _networkManager.SceneManager.LoadScene(ShooterScenes.SHOOTER2_X2_SCENE, LoadSceneMode.Single);
        }
    }
}
