using UnityEngine;

namespace Game.Application
{
    public sealed class AppConfigurationProvider : Singleton<AppConfigurationProvider>
    {
        public AppConfiguration Configuration => _appConfiguration;

        [SerializeField]
        private DefaultAppConfiguration _defaultAppConfiguration;
        [SerializeField]
        private YGAppConfiguration _ygAppConfiguration;
        private AppConfiguration _appConfiguration;

        protected async override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
#if UNITY_EDITOR
            _appConfiguration = _defaultAppConfiguration;
#elif UNITY_WEBGL
           _appConfiguration = _ygAppConfiguration;
            await _ygAppConfiguration.Init();
#endif
            Debug.Log($"App configuration chosen: {_appConfiguration.GetType()}");
        }
    }
}
