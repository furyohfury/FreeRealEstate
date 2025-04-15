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
			
			Container.Bind<CellChooser>()
			         .AsSingle();

			Container.Bind<UniqueCellsProvider>()
			         .AsSingle();

			Container.BindInterfacesAndSelfTo<LevelCellsSwitcher>()
			         .AsSingle();

			Container.BindInterfacesAndSelfTo<LevelsLoopController>()
			         .AsSingle();
		}
	}
}