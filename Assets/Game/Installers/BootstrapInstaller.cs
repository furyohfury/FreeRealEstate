using Game.App;
using Game.Network;
using Gameplay;
using Zenject;

namespace Installers
{
	public sealed class BootstrapInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<PlayerNickname>()
			         .AsSingle();

			Container.Bind<MyPlayerService>()
			         .AsSingle();

			Container.BindInterfacesTo<MyPlayerController>()
			         .AsSingle();

			Container.BindInterfacesAndSelfTo<SessionSystem>()
			         .AsSingle();
		}
	}
}