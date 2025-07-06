using UnityEngine;
using VFX;
using Zenject;

namespace Game.VFX
{
	[CreateAssetMenu(fileName = "VFXFactoriesInstaller", menuName = "Create installer/VFX/VFXFactoriesInstaller")]
	public sealed class VFXFactoriesInstaller : ScriptableObjectInstaller
	{
		[SerializeField]
		private ParticleSystemVFXFactory[] _vfxSystems;
		
		public override void InstallBindings()
		{
			InstallParticleSystemFactories();
		}

		private void InstallParticleSystemFactories()
		{
			for (int i = 0, count = _vfxSystems.Length; i < count; i++)
			{
				Container.Bind<IVFXFactory>()
				         .To<ParticleSystemVFXFactory>()
				         .FromInstance(_vfxSystems[i])
				         .AsCached();
			}
		}
	}
}