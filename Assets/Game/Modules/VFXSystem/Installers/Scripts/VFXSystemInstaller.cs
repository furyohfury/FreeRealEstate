using UnityEngine;
using VFX;
using Zenject;

namespace Game.VFX
{
	public sealed class VFXSystemInstaller : MonoInstaller
	{
		[SerializeField]
		private VFXSystem _vfxSystem;
		
		public override void InstallBindings()
		{
			Container.Bind<VFXSystem>()
			         .FromInstance(_vfxSystem)
			         .AsSingle();
		}
	}
}