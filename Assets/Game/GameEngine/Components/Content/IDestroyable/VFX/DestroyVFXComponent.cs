using System;
using UnityEngine;
using VFX;

namespace GameEngine
{
	[Serializable]
	public sealed class DestroyVFXComponent
	{
		[SerializeField]
		private VFXType _type;
		[SerializeField]
		private Transform _point;

		public void PlayVFX()
		{
			VFXSystem.Instance.PlayAndDestroyVFX(_type, _point.position);
		}
	}
}