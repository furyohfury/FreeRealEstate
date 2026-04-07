using Newtonsoft.Json;
using TriInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public sealed class JSONDebug : MonoBehaviour
    {
        [FormerlySerializedAs("sessionParams")]
        public SessionParamsConfig sessionParamsConfig;

        [Button]
        private void Log()
        {
            string serializeObject = JsonConvert.SerializeObject(sessionParamsConfig);
            Debug.Log(serializeObject);
        }
    }
}
