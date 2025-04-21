using UnityEngine;
using Zenject;

namespace Game
{
	public sealed class CoreInstaller : MonoInstaller
	{
		[SerializeField] 
		private CellBundle[] _cellLists;

		public override void InstallBindings()
		{
			Container.Bind<CellBundle[]>()
			         .FromInstance(_cellLists)
			         .AsCached();

			Container.Bind<ActiveBundleService>()
			         .AsSingle();
			
			Container.Bind<CellChooser>()
			         .AsSingle();

			Container.Bind<DesiredCellController>()
			         .AsSingle();
			
			Container.BindInterfacesAndSelfTo<UniqueCellsMemorizer>()
			         .AsSingle();

			Container.BindInterfacesAndSelfTo<BundleSwitcher>()
			         .AsSingle();

			// Container.BindInterfacesAndSelfTo<BundlesLoopController>()
			//          .AsSingle();
		}
	}
}