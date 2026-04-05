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

        public override float GetMaxHealth()
        {
            if (YG2.TryGetFlagAsFloat(YGAppConfigurationFlags.MAX_HP, out float value))
            {
                Debug.Log($"<color=green>Got flag {YGAppConfigurationFlags.MAX_HP} from YG</color>");
                return value;
            }

            Debug.LogWarning($"Couldnt get flag {YGAppConfigurationFlags.MAX_HP} from YG. Taking default</color>");
            return _maxHealth;
        }

        public override bool GetTrackDeath()
        {
            if (YG2.TryGetFlagAsBool(YGAppConfigurationFlags.TRACK_DEATH, out bool value))
            {
                Debug.Log($"<color=green>Got flag {YGAppConfigurationFlags.TRACK_DEATH} from YG</color>");
                return value;
            }

            Debug.LogWarning($"Couldnt get flag {YGAppConfigurationFlags.TRACK_DEATH} from YG. Taking default</color>");
            return _trackDeath;
        }

        public override float GetMusicVolumeMult()
        {
            if (YG2.TryGetFlagAsFloat(YGAppConfigurationFlags.MUSIC_VOLUME_MULT, out float value))
            {
                Debug.Log($"<color=green>Got flag {YGAppConfigurationFlags.MUSIC_VOLUME_MULT} from YG</color>");
                return value;
            }

            Debug.LogWarning($"Couldnt get flag {YGAppConfigurationFlags.MUSIC_VOLUME_MULT} from YG. Taking default</color>");
            return _musicVolume;
        }
    }
}
