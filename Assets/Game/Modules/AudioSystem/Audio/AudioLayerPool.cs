using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Object = UnityEngine.Object;

namespace Audio
{
    public sealed class AudioLayerPool: IDisposable
    {
        private readonly AudioMixerGroup _audioMixerGroup;
        private readonly List<AudioLayer> _poolObjects;
        private readonly Transform _poolRoot;
        private readonly AudioLayerFactory _audioLayerFactory;

        public AudioLayerPool(AudioMixerGroup audioMixerGroup, AudioLayerSetting audioLayerSetting)
        {
            _audioMixerGroup = audioMixerGroup;
            _poolRoot = new GameObject(nameof(AudioLayerPool)).transform;
            _poolObjects = new List<AudioLayer>(16);
            _audioLayerFactory = new AudioLayerFactory(audioLayerSetting);
        }

        public AudioLayer Get(Vector3 position)
        {
            for (var index = 0; index < _poolObjects.Count; index++)
            {
                AudioLayer audioLayer = _poolObjects[index];
                if (audioLayer.IsPlaying() == false)
                {
                    audioLayer.SetPosition(position);
                    return audioLayer;
                }
            }

            AudioLayer layer = _audioLayerFactory.CreateAudioLayer(AudioOutput.None, _audioMixerGroup, _poolRoot);
            layer.SetPosition(position);
            _poolObjects.Add(layer);
            return layer;
        }

        public void Dispose()
        {
            for (var index = 0; index < _poolObjects.Count; index++)
            {
                AudioLayer audioLayer = _poolObjects[index];
                audioLayer.Dispose();
            }

            Object.Destroy(_poolRoot.gameObject);
        }
    }
}
