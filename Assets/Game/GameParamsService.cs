using Game.Application;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public sealed class GameParamsService : Singleton<GameParamsService>
    {
        [field: FormerlySerializedAs("<SessionParams>k__BackingField")]
        [field: SerializeField]
        public SessionParams SessionParams { get; set; }

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            SessionParamsStorage sessionParamsStorage = AppConfigurationProvider.Instance.Configuration.GetSessionParamsStorage();
            SessionParams = sessionParamsStorage.SessionParams[0];
        }
    }
}
