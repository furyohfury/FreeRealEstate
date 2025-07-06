using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameEngine
{
	[Serializable]
	public sealed class AttackSFXComponent
	{
		[SerializeField]
		private AudioSource _audioSource;
		[SerializeField]
		private AudioClip[] _audioClips;

		public void PlaySFX()
		{
			_audioSource.PlayOneShot(_audioClips[Random.Range(0, _audioClips.Length)]);
		}
	}
}