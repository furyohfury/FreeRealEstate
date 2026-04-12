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
        [SerializeField]
        [Range(0f, 1f)]
        private float _musicVolume = 0.25f;
        [SerializeField]
        private float _dayMainLightIntensity = 0.35f;
        [SerializeField]
        private float _nightMainLightIntensity = 0.15f;
        [SerializeField]
        [Required]
        private SessionParamsStorageConfig _sessionParamsStorageConfig;
        [SerializeField]
        private string _sessionParamsStorageURL =
            "https://raw.githubusercontent.com/furyohfury/FreeRealEstate/refs/heads/conveyors-yandex/Assets/StreamingAssets/SessionParamsStorage.json";
        private SessionParamsStorage _sessionParamsStorage;
        [SerializeField]
        private string _qualityLevelName = "Low";
        [SerializeField]
        private bool _isUsingQualityLevel = false;

        public async Awaitable Init()
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

            if (YG2.TryGetFlagAsFloat(YGAppConfigurationFlags.DAY_MAIN_LIGHT_INTENSITY, out _dayMainLightIntensity))
            {
                Debug.Log($"<color=green>Got flag {YGAppConfigurationFlags.DAY_MAIN_LIGHT_INTENSITY} from YG</color>");
            }
            else
            {
                Debug.LogWarning($"Couldnt get flag {YGAppConfigurationFlags.DAY_MAIN_LIGHT_INTENSITY} from YG. Taking default</color>");
            }

            if (YG2.TryGetFlagAsFloat(YGAppConfigurationFlags.NIGHT_MAIN_LIGHT_INTENSITY, out _nightMainLightIntensity))
            {
                Debug.Log($"<color=green>Got flag {YGAppConfigurationFlags.NIGHT_MAIN_LIGHT_INTENSITY} from YG</color>");
            }
            else
            {
                Debug.LogWarning($"Couldnt get flag {YGAppConfigurationFlags.NIGHT_MAIN_LIGHT_INTENSITY} from YG. Taking default</color>");
            }

            if (YG2.TryGetFlag(YGAppConfigurationFlags.QUALITY_LEVEL, out _qualityLevelName))
            {
                Debug.Log($"<color=green>Got flag {YGAppConfigurationFlags.QUALITY_LEVEL} from YG</color>");
            }
            else
            {
                Debug.LogWarning($"Couldnt get flag {YGAppConfigurationFlags.QUALITY_LEVEL} from YG. Taking default</color>");
            }

            SessionParamsStorage sessionParamsStorage = await WebConfigLoader.LoadConfigAsync(_sessionParamsStorageURL);

            if (sessionParamsStorage != null)
            {
                Debug.Log("Session params storage loaded from web");
                _sessionParamsStorage = sessionParamsStorage;
            }
            else
            {
                Debug.LogError("Session params storage wasnt loaded from web. Taking default");
                _sessionParamsStorage = _sessionParamsStorageConfig.GetStorage();
            }

            if (YG2.TryGetFlagAsBool(YGAppConfigurationFlags.IS_USING_QUALITY_LEVEL, out _isUsingQualityLevel))
            {
                Debug.Log($"<color=green>Got flag {YGAppConfigurationFlags.IS_USING_QUALITY_LEVEL} from YG</color>");
            }
            else
            {
                Debug.LogWarning($"Couldnt get flag {YGAppConfigurationFlags.IS_USING_QUALITY_LEVEL} from YG. Taking default</color>");
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

        public override float GetDayMainLightIntensity()
        {
            return _dayMainLightIntensity;
        }

        public override float GetNightMainLightIntensity()
        {
            return _nightMainLightIntensity;
        }

        public override bool GetIsUsingQualityLevel()
        {
            return _isUsingQualityLevel;
        }

        public override string GetQualityLevelName()
        {
            return _qualityLevelName;
        }

        public override SessionParamsStorage GetSessionParamsStorage()
        {
            return _sessionParamsStorage;
        }
    }
}
