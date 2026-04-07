using System;
using System.Linq;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "SessionParamsStorage", menuName = "Game/SessionParamsStorage")]
    public sealed class SessionParamsStorageConfig : ScriptableObject
    {
        [SerializeField]
        private SessionParamsConfig[] _sessionParamConfigs;
        private SessionParamsStorage _sessionParamsStorage;

        private void OnEnable()
        {
            _sessionParamsStorage = null;
        }

        public SessionParamsStorage GetStorage()
        {
            if (_sessionParamsStorage == null)
            {
                SessionParams[] sessionParams = _sessionParamConfigs.Select(config => config.GetSessionParams()).ToArray();
                _sessionParamsStorage = new SessionParamsStorage(sessionParams);
            }

            return _sessionParamsStorage;
        }
    }
}
