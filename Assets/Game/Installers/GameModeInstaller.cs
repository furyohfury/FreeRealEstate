using Game.UI;
using Zenject;

namespace Installers
{
	public sealed class GameModeInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<GameModeView>()
			         .FromComponentInHierarchy()
			         .AsSingle();

			Container.BindInterfacesTo<GameModePresenter>()
			         .AsSingle();
		}
	}
}