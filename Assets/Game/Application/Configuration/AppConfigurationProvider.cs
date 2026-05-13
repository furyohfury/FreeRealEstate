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
#if YANDEX_GAMES_BUILD
           _appConfiguration = _ygAppConfiguration;
            await _ygAppConfiguration.Init();
#else
            _appConfiguration = _defaultAppConfiguration;
#endif
            Debug.Log($"App configuration chosen: {_appConfiguration.GetType()}");
        }
    }
}
