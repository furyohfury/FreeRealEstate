using UnityEngine;
using Zenject;

namespace Game
{
	public sealed class CoreInstaller : MonoInstaller
	{
		[SerializeField] 
		private CellList[] _cellLists;

		public override void InstallBindings()
		{
			Container.Bind<CellList[]>()
			         .FromInstance(_cellLists)
			         .AsCached();

			Container.Bind<ActiveLevelService>()
			         .AsSingle();
			
			Container.Bind<CellChooser>()
			         .AsSingle();

			Container.BindInterfacesAndSelfTo<UniqueCellsProvider>()
			         .AsSingle();

			Container.BindInterfacesAndSelfTo<LevelCellsSwitcher>()
			         .AsSingle();

			Container.BindInterfacesAndSelfTo<LevelsLoopController>()
			         .AsSingle();
		}
	}
}