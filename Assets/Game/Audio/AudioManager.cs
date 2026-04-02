using System.Collections.Generic;
using Game.Extensions;
using UnityEngine;

namespace Game
{
    public sealed class AudioManager : Singleton<AudioManager>
    {
        [field: SerializeField]
        [field: Range(0, 1f)]
        public float Volume { get; set; }
        [SerializeField]
        private float _spatialBlend = 1f;

        [SerializeField]
        private AudioPool _audioPool;
        [Header("Clips")]
        [SerializeField]
        private AudioClip _itemSpawnClip;
        [SerializeField]
        private AudioClip _itemDestroyClip;
        [SerializeField]
        private AudioClip[] _collisionClips;
        [SerializeField]
        private AudioClip _rightColorItemConsumedClip;
        [SerializeField]
        private AudioClip _wrongColorItemConsumedClip;
        [SerializeField]
        private AudioClip _itemSwipeCLip;
        [SerializeField]
        private AudioClip[] _buttonClickClips;
        [SerializeField]
        private AudioClip _gameOverClip;

        private readonly HashSet<AudioSource> _activeSources = new HashSet<AudioSource>();

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        public void PlayItemSpawnSound(Vector3 position)
        {
            Launch3DClipAsync(_itemSpawnClip, position);
        }

        public void PlayDestroyItemSFX(Vector3 position)
        {
            Launch3DClipAsync(_itemDestroyClip, position);
        }

        public void PlayCollisionSFX(Vector3 position)
        {
            Launch3DClipAsync(_collisionClips.GetRandom(), position);
        }

        public void PlayRightColorItemConsumedSFX(Vector3 position)
        {
            Launch3DClipAsync(_rightColorItemConsumedClip, position);
        }

        public void PlayWrongColorItemConsumedSFX(Vector3 position)
        {
            Launch3DClipAsync(_wrongColorItemConsumedClip, position);
        }

        public void PlaySwipeItemSound(Vector3 position)
        {
            Launch3DClipAsync(_itemSwipeCLip, position);
        }

        public void PlayClickButtonSound()
        {
            Launch2DClipAsync(_buttonClickClips.GetRandom());
        }

        public void PlayGameOverSound()
        {
            Launch2DClipAsync(_gameOverClip);
        }

        private async Awaitable Launch3DClipAsync(AudioClip clip, Vector3 position)
        {
            AudioSource source = _audioPool.GetSource();
            source.clip = clip;
            source.volume = Volume;
            source.transform.position = position;
            source.spatialBlend = _spatialBlend;
            source.Play();
            _activeSources.Add(source);

            await Awaitable.WaitForSecondsAsync(clip.length);

            _audioPool.Return(source);
        }

        private async Awaitable Launch2DClipAsync(AudioClip clip)
        {
            AudioSource source = _audioPool.GetSource();
            source.clip = clip;
            source.volume = Volume;
            source.spatialBlend = 0;
            source.Play();
            _activeSources.Add(source);

            await Awaitable.WaitForSecondsAsync(clip.length);

            _audioPool.Return(source);
        }
    }
}
