using System;
using Audio;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
	[Serializable]
	public sealed class PikminInteractSFXComponent
	{
		[SerializeField]
		private AudioClip[] _audioClips;
		[SerializeField]
		private Transform _point;
		[SerializeField] [Range(0, 1)]
		private float _volume = 0.4f;

		public void PlaySFX()
		{
			var randomClip = _audioClips[Random.Range(0, _audioClips.Length)];
			AudioManager.Instance.PlaySoundOneShot(randomClip, _point.position, _volume);
		}
	}
}