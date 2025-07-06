using UnityEngine;

namespace VFX
{
	public interface IVFXFactory
	{
		VFXType GetVFXType();
		IVFX Spawn();
	}
}