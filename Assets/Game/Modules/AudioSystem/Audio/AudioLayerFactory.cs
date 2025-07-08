using UnityEngine;
using UnityEngine.Audio;
using Utils;

namespace Audio
{
    public sealed class AudioLayerFactory
    {
        private readonly AudioLayerSetting _setting;

        public AudioLayerFactory(AudioLayerSetting setting)
        {
            _setting = setting;
        }
        
        public AudioLayer CreateAudioLayer(AudioOutput type, AudioMixerGroup audioMixerGroup, Transform root)
        {
            var gameObject = new GameObject();
            gameObject.transform.SetParent(root);
            gameObject.name = EnumUtils<AudioOutput>.ToString(type);
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.outputAudioMixerGroup = audioMixerGroup;
            _setting.SetSettings(type, source);
            bool isWaitingPlayingEnd = _setting.GetIsWaitingPlayingEnd(type);

            AudioLayer layer = new AudioLayer(source, isWaitingPlayingEnd);

            return layer;
        }
    }
}
