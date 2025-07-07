using UnityEngine;
using VFX;

namespace Game.VFX
{
	[CreateAssetMenu(
		fileName = "ParticleSystemVFXFactory",
		menuName = "VFX system/VFX factory/ParticleSystemVFXFactory"
	)]
	public sealed class ParticleSystemVFXFactory : ScriptableObject, IVFXFactory
	{
		[SerializeField]
		private VFXType _type;
		[SerializeField]
		private ParticleSystemVFX _prefab;

		public string GetVFXType()
		{
			return _type.ID;
		}

		public IVFX Spawn(Vector3 pos, Quaternion rot, Transform parent)
		{
			var vfx = Instantiate(_prefab, parent);
			vfx.Move(pos, Quaternion.identity);
			return vfx;
		}
	}
}