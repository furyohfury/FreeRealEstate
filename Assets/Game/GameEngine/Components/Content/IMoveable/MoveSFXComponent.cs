using System;
using UnityEngine;

namespace GameEngine
{
	[Serializable]
	public sealed class MoveSFXComponent
	{
		[SerializeField]
		private AudioSource _audioSource;

		public void Play()
		{
			if (_audioSource.isPlaying == false)
			{
			_audioSource.Play();
				
			}
		}

		public void Stop()
		{
			_audioSource.Stop();
		}
	}
}