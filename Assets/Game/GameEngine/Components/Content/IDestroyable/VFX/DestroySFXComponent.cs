using System;
using UnityEngine;

namespace GameEngine
{
	[Serializable]
	public sealed class DestroySFXComponent
	{
		[SerializeField]
		private AudioSource _audioSource;

		public void PlaySFX()
		{
			if (_audioSource != null)
			{
				_audioSource.Play();
			}
		}
	}
}