using System;
using Audio;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameEngine
{
	[Serializable]
	public sealed class AttackSFXComponent
	{
		[SerializeField]
		private AudioClip[] _audioClips;
		[SerializeField]
		private Transform _point;
		[SerializeField] [Range(0, 1)]
		private float _volume = 0.4f;

		public void Play()
		{
			var randomClip = _audioClips[Random.Range(0, _audioClips.Length)];
			AudioManager.Instance.PlaySoundOneShot(randomClip, _point.position, _volume);
		}
	}
}