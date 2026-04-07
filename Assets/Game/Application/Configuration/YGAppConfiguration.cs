using TriInspector;
using UnityEngine;
using YG;

namespace Game.Application
{
    [CreateAssetMenu(fileName = "YGAppConfiguration", menuName = "Game/AppConfig/YGAppConfiguration")]
    public sealed class YGAppConfiguration : AppConfiguration
    {
        [SerializeField]
        private float _maxHealth = 100f;
        [SerializeField]
        private bool _trackDeath = true;
        [SerializeField] [Range(0f, 1f)]
        private float _musicVolume = 0.25f;
        [SerializeField] [Required]
        private SessionParamsStorageConfig _sessionParamsStorageConfig;
        private SessionParamsStorage _sessionParamsStorage;
        [SerializeField]
        private string _sessionParamsStorageURL =
            "https://raw.githubusercontent.com/furyohfury/FreeRealEstate/refs/heads/conveyors-yandex/Assets/StreamingAssets/SessionParamsStorage.json";

        public async void Init()
        {
            if (YG2.TryGetFlagAsFloat(YGAppConfigurationFlags.MAX_HP, out _maxHealth))
            {
                Debug.Log($"<color=green>Got flag {YGAppConfigurationFlags.MAX_HP} from YG</color>");
            }
            else
            {
                Debug.LogWarning($"Couldnt get flag {YGAppConfigurationFlags.MAX_HP} from YG. Taking default</color>");
            }

            if (YG2.TryGetFlagAsBool(YGAppConfigurationFlags.TRACK_DEATH, out _trackDeath))
            {
                Debug.Log($"<color=green>Got flag {YGAppConfigurationFlags.TRACK_DEATH} from YG</color>");
            }
            else
            {
                Debug.LogWarning($"Couldnt get flag {YGAppConfigurationFlags.TRACK_DEATH} from YG. Taking default</color>");
            }

            if (YG2.TryGetFlagAsFloat(YGAppConfigurationFlags.MUSIC_VOLUME_MULT, out _musicVolume))
            {
                Debug.Log($"<color=green>Got flag {YGAppConfigurationFlags.MUSIC_VOLUME_MULT} from YG</color>");
            }
            else
            {
                Debug.LogWarning($"Couldnt get flag {YGAppConfigurationFlags.MUSIC_VOLUME_MULT} from YG. Taking default</color>");
            }

            SessionParamsStorage sessionParamsStorage = await WebConfigLoader.LoadConfigAsync(_sessionParamsStorageURL);

            if (sessionParamsStorage != null)
            {
                _sessionParamsStorage = sessionParamsStorage;
            }
            else
            {
                _sessionParamsStorage = _sessionParamsStorageConfig.GetStorage();
            }
        }

        public override float GetMaxHealth()
        {
            return _maxHealth;
        }

        public override bool GetTrackDeath()
        {
            return _trackDeath;
        }

        public override float GetMusicVolumeMult()
        {
            return _musicVolume;
        }

        public override SessionParamsStorage GetSessionParamsStorage()
        {
            return _sessionParamsStorage;
        }
    }
}
