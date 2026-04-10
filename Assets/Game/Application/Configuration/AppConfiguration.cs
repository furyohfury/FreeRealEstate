using UnityEngine;

namespace Game.Application
{
    public abstract class AppConfiguration : ScriptableObject
    {
        public abstract float GetMaxHealth();
        public abstract bool GetTrackDeath();
        public abstract float GetMusicVolumeMult();
        public abstract float GetDayMainLightIntensity();
        public abstract float GetNightMainLightIntensity();
        public abstract SessionParamsStorage GetSessionParamsStorage();
    }
}
