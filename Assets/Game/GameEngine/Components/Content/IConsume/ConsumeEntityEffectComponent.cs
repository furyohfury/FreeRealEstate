using System;
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
		private AudioSource _source;
		private IVFX _activeVFX;

		public void PlayEffect()
		{
			_source.Play();
			_activeVFX = VFXSystem.Instance.PlayVFX(_type.ID, _point.position, Quaternion.identity);
		}

		public void StopEffect()
		{
			_source.Stop();
			_activeVFX.Remove();
		}
	}
}