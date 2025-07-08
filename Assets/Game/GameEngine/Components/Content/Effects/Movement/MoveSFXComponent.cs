using System;
using Audio;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameEngine
{
	[Serializable]
	public sealed class MoveSFXComponent
	{
		[SerializeField]
		private AudioClip[] _audioClips;
		[SerializeField]
		private Transform _point;
		[SerializeField] [Range(0, 1)]
		private float _volume = 0.4f;

		private float[] _durations;
		private bool _isPlaying;

		public async UniTask Play()
		{
			if (_durations == null)
			{
				InitDurations();
			}

			if (_isPlaying)
			{
				return;
			}

			_isPlaying = true;
			while (_isPlaying)
			{
				var randomIndex = Random.Range(0, _audioClips.Length);
				AudioManager.Instance.PlaySoundOneShot(_audioClips[randomIndex], _point.position, _volume);
				await UniTask.Delay(TimeSpan.FromSeconds(_durations![randomIndex]));
			}
		}

		public void Stop()
		{
			_isPlaying = false;
		}

		private void InitDurations()
		{
			_durations = new float[_audioClips.Length];
			for (int i = 0, count = _durations.Length; i < count; i++)
			{
				_durations[i] = _audioClips[i].length;
			}
		}
	}
}