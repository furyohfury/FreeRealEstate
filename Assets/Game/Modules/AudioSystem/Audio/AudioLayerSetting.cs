using System;
using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = nameof(AudioLayerSetting), menuName = "Data/Audio/LayerSetting")]
    public class AudioLayerSetting : ScriptableObject
    {
        [Serializable]
        private struct SettingsByAudioOutput
        {
            public AudioOutput Type;
            public bool IsWaitingPlayingEnd;
            public bool IsLoop;
        }

        [SerializeField] private SettingsByAudioOutput[] _settingsByAudioOutputs;

        public void SetSettings(AudioOutput type, AudioSource source)
        {
            SettingsByAudioOutput settings = GetSettingsByAudioOutput(type);
            source.loop = settings.IsLoop;
        }

        public bool GetIsWaitingPlayingEnd(AudioOutput type)
        {
            SettingsByAudioOutput settings = GetSettingsByAudioOutput(type);
            return settings.IsWaitingPlayingEnd;
        }

        private SettingsByAudioOutput GetSettingsByAudioOutput(AudioOutput type)
        {
            for (var index = 0; index < _settingsByAudioOutputs.Length; index++)
            {
                SettingsByAudioOutput output = _settingsByAudioOutputs[index];
                if (output.Type == type)
                {
                    return output;
                }
            }

            throw new InvalidOperationException($"Not found settings for {type}");
        }
    }
}
