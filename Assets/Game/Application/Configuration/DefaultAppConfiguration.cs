using System.Linq;
using UnityEngine;

namespace Game.Application
{
    [CreateAssetMenu(fileName = "DefaultAppConfiguration", menuName = "Game/AppConfig/DefaultAppConfiguration")]
    public sealed class DefaultAppConfiguration : AppConfiguration
    {
        [SerializeField]
        private float _maxHealth = 100f;
        [SerializeField]
        private bool _trackDeath = false;
        [SerializeField] [Range(0f, 1f)]
        private float _musicVolumeMultiplier = 0.5f;
        [SerializeField]
        private SessionParamsStorageConfig _sessionParamsStorageConfig;

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
            return _musicVolumeMultiplier;
        }

        public override SessionParamsStorage GetSessionParamsStorage()
        {
            return _sessionParamsStorageConfig.GetStorage();
        }
    }
}
