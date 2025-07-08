using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Audio
{
    public sealed class AudioLayer : IDisposable
    {
        private readonly AudioSource _audioSource;
        private readonly bool _isWaitingPlayingEnd;

        public AudioLayer(AudioSource source, bool isWaitingPlayingEnd)
        {
            _audioSource = source;
            _isWaitingPlayingEnd = isWaitingPlayingEnd;
        }

        public bool IsPlaying()
        {
            return _audioSource.isPlaying;
        }

        public void PlayOneShot(AudioClip clip, float volumeScale, float pitch)
        {
            _audioSource.pitch = pitch;
            _audioSource.PlayOneShot(clip, volumeScale);
        }

        public void Play(AudioClip clip, float volumeScale = 1f)
        {
            if (_isWaitingPlayingEnd && _audioSource.isPlaying)
            {
                return;
            }

            _audioSource.clip = clip;
            _audioSource.volume = volumeScale;
            _audioSource.Play();
        }

        public void SetPosition(Vector3 position)
        {
            _audioSource.transform.position = position;
        }

        public void Dispose()
        {
            Object.Destroy(_audioSource.gameObject);
        }
    }
}
