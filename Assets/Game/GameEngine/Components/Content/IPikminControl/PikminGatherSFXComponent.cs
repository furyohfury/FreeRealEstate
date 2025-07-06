using System;
using UnityEngine;

namespace Game
{
	[Serializable]
	public sealed class PikminGatherSFXComponent
	{
		[SerializeField]
		private AudioSource _audioSource;

		public void PlaySFX()
		{
			if (_audioSource.isPlaying)
			{
				return;
			}

			_audioSource.Play();
		}

		public void StopSFX()
		{
			_audioSource.Stop();
		}
	}
}