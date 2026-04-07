using Newtonsoft.Json;
using TriInspector;
using UnityEngine;

namespace Game
{
    public sealed class JSONDebug : MonoBehaviour
    {
        [SerializeField]
        private SessionParamsStorageConfig sessionParamsStorageConfig;
        [SerializeField]
        private string sessionParamsJson;
        [SerializeField]
        private SessionParams[] _sessionParams;
        [SerializeField]
        private string _sessionParamsStorageURL =
            "https://raw.githubusercontent.com/furyohfury/FreeRealEstate/refs/heads/conveyors-yandex/Assets/StreamingAssets/SessionParamsStorage.json";

        [Button]
        private void Log()
        {
            var settings = new JsonSerializerSettings
                           {
                               TypeNameHandling = TypeNameHandling.Auto
                           };
            sessionParamsJson = JsonConvert.SerializeObject(sessionParamsStorageConfig.GetStorage(), settings);
            Debug.Log(sessionParamsJson);
        }

        [Button]
        private void DeserializeSessionParams()
        {
            var settings = new JsonSerializerSettings
                           {
                               TypeNameHandling = TypeNameHandling.Auto
                           };
            SessionParamsStorage sessionParamsStorage = JsonConvert.DeserializeObject<SessionParamsStorage>(sessionParamsJson, settings);
            _sessionParams = sessionParamsStorage.SessionParams;
        }

        [Button]
        private async Awaitable LoadFromWeb()
        {
            SessionParamsStorage sessionParamsStorage = await WebConfigLoader.LoadConfigAsync(_sessionParamsStorageURL);

            if (sessionParamsStorage == null)
            {
                Debug.Log($"<color=red>loaded null</color>");
            }
            else
            {
                _sessionParams = sessionParamsStorage.SessionParams;
            }
        }
    }
}
