using Zenject;

namespace Game
{
	public sealed class RenderingPresentersInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<CellPresenterFactory>()
			         .AsSingle();
			
			Container.BindInterfacesAndSelfTo<CellViewListPresenter>()
			         .AsSingle();
		}
	}
}