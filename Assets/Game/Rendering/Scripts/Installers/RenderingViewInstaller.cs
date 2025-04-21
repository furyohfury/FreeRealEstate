using UnityEngine;
using Zenject;

namespace Game
{
	public sealed class RenderingViewInstaller : MonoInstaller
	{
		[SerializeField]
		private CellViewList _cellViewList;
		[SerializeField]
		private CellView _cellPrefab;
		
		public override void InstallBindings()
		{
			Container.Bind<CellView>()
			         .FromInstance(_cellPrefab)
			         .AsSingle();
			
			Container.Bind<CellViewList>()
			         .FromInstance(_cellViewList)
			         .AsSingle();

			Container.Bind<CellViewFactory>()
			         .AsSingle();
		}
	}
}