using UnityEngine;

namespace Game
{
    public class WebConfigLoadDebug : MonoBehaviour
    {
        [SerializeField]
        private string _sessionParamsStorageURL =
            "https://raw.githubusercontent.com/furyohfury/FreeRealEstate/refs/heads/conveyors-yandex/Assets/StreamingAssets/SessionParamsStorage.json";

        private async void Awake()
        {
            SessionParamsStorage sessionParamsStorage = await WebConfigLoader.LoadConfigAsync(_sessionParamsStorageURL);

            Debug.Log(sessionParamsStorage == null);
        }
    }
}
