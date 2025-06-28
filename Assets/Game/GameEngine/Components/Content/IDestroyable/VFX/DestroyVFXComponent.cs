using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameEngine
{
	[Serializable]
	public sealed class DestroyVFXComponent
	{
		[SerializeField]
		private ParticleSystem _vfx;
		[SerializeField]
		private Transform _spawnPosition;

		public void SpawnVFX()
		{
			Object.Instantiate(_vfx, _spawnPosition);
		}
	}
}