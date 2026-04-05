using UnityEngine;

namespace Game.Application
{
    public sealed class AppConfigurationProvider : Singleton<AppConfigurationProvider>
    {
        public AppConfiguration Configuration => _appConfiguration;

        [SerializeField]
        private DefaultAppConfiguration _defaultAppConfiguration;
        private AppConfiguration _appConfiguration;

        protected override void Awake()
        {
            base.Awake();
#if UNITY_EDITOR
            _appConfiguration = _defaultAppConfiguration;
// #elif UNITY_WEBGL
//            _appConfiguration = new YGAppConfiguration();
#endif
        }
    }
}
