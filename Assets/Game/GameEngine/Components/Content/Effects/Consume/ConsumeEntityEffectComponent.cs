using System;
using Audio;
using UnityEngine;
using VFX;

namespace GameEngine
{
	[Serializable]
	public sealed class ConsumeEntityEffectComponent
	{
		[SerializeField]
		private VFXType _type;
		[SerializeField]
		private Transform _point;
		[SerializeField]
		private AudioClip _clip;
		[SerializeField][Range(0, 1f)]
		private float _volume = 0.4f;
		
		private IVFX _activeVFX;

		public void Play()
		{
			AudioManager.Instance.PlaySoundOneShot(_clip, _point.position, _volume);
			_activeVFX = VFXSystem.Instance.PlayVFX(_type.ID, _point.position, Quaternion.identity);
		}

		public void Stop()
		{
			_activeVFX.Remove();
		}
	}
}