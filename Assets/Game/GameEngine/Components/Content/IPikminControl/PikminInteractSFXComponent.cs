using System;
using UnityEngine;

namespace Game
{
	[Serializable]
	public sealed class PikminInteractSFXComponent
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
	}
}