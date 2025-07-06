using UnityEngine;
using VFX;

namespace Game.VFX
{
	[CreateAssetMenu(fileName = "ParticleSystemVFXFactory", menuName = "Create VFX factory/ParticleSystemVFXFactory")]
	public sealed class ParticleSystemVFXFactory : ScriptableObject, IVFXFactory
	{
		[SerializeField]
		private VFXType _type;
		[SerializeField]
		private ParticleSystemVFX _prefab;

		public VFXType GetVFXType()
		{
			return _type;
		}

		public IVFX Spawn()
		{
			return Instantiate(_prefab);
		}
	}
}