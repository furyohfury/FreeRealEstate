using Newtonsoft.Json;
using TriInspector;
using UnityEngine;

namespace Game
{
    public sealed class JSONDebug : MonoBehaviour
    {
        public SessionParams sessionParams;

        [Button]
        private void Log()
        {
            string serializeObject = JsonConvert.SerializeObject(sessionParams);
            Debug.Log(serializeObject);
        }
    }
}
