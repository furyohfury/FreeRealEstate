using UnityEngine;

namespace VFX
{
	public interface IVFXFactory
	{
		string GetVFXType();
		IVFX Spawn(Vector3 pos, Quaternion rot, Transform parent);
	}
}