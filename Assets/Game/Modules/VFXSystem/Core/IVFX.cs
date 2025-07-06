using System;
using UnityEngine;

namespace VFX
{
	public interface IVFX
	{
		event Action<IVFX> OnVFXEnd;
		void Play();
		void Move(Vector3 pos, Quaternion rot);
		void Remove();
	}
}