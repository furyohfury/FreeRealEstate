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

		public void PlayEffect()
		{
			_source.Play();
			VFXSystem.Instance.PlayAndDestroyVFX(_type, _point.position);
		}

		public void StopEffect()
		{
			_source.Stop();
		}
	}
}