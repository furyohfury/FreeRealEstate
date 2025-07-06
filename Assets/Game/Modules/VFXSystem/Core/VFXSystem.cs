using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace VFX
{
	public sealed class VFXSystem : MonoBehaviour
	{
		public static VFXSystem Instance;
		private readonly Dictionary<VFXType, IVFXFactory> _factories = new();

		[Inject]
		public void Construct(IEnumerable<IVFXFactory> factories)
		{
			foreach (var factory in factories)
			{
				var vfxType = factory.GetVFXType();
				if (_factories.TryAdd(vfxType, factory) == false)
				{
					throw new ArgumentException($"Double spawned factory of type {vfxType}");
				}
			}
		}

		private void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
			}
			else
			{
				Destroy(this);
				throw new Exception("Instantiated more than one VFXSystem");
			}
		}

		public IVFX PlayVFX(VFXType type, Vector3 pos)
		{
			if (_factories.TryGetValue(type, out var factory))
			{
				var vfx = factory.Spawn();
				vfx.Move(pos, Quaternion.identity);
				vfx.Play();
				return vfx;
			}

			throw new ArgumentException($"No factory with type {type}");
		}

		public IVFX PlayAndDestroyVFX(VFXType type, Vector3 pos)
		{
			if (_factories.TryGetValue(type, out var factory))
			{
				var vfx = factory.Spawn();
				
				vfx.Play();
				vfx.Move(pos, Quaternion.identity);
				vfx.OnVFXEnd += Remove;
				return vfx;
			}

			throw new ArgumentException($"No factory with type {type}");
		}

		private void Remove(IVFX vfx)
		{
			vfx.Remove();
		}
	}
}