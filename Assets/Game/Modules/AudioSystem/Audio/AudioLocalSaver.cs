using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Audio
{
    public sealed class AudioLocalSaver
    {
        private const string AUDIO_SETTINGS_KEY = "audio_settings";
        private readonly Dictionary<string, float> _audioSettings;
        public AudioLocalSaver()
        {
            string items = PlayerPrefs.GetString(AUDIO_SETTINGS_KEY, String.Empty);
            _audioSettings = string.IsNullOrEmpty(items) ? new Dictionary<string, float> () : ParseUtils.StringToStringFloatDictionary(items);
        }

        public void Save(string channelName, float volume)
        {
            _audioSettings[channelName] = volume;

            string audioSettings = ParseUtils.ConvertDictionaryStringFloatToString(_audioSettings);
            PlayerPrefs.SetString(AUDIO_SETTINGS_KEY, audioSettings);

            PlayerPrefs.Save();
        }

        public float GetVolume(string channelName)
        {
            if (_audioSettings.TryGetValue(channelName, out float volume))
            {
                return volume;
            }

            return AudioManagerStaticData.CHANNEL_VOLUME_DEFAULT;
        }
        
        public void Clear()
        {
            PlayerPrefs.DeleteKey(AUDIO_SETTINGS_KEY);
        }
    }
}
