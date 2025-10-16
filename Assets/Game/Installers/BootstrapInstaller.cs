using Game.App;
using Game.Network;
using Zenject;

namespace Installers
{
	public sealed class BootstrapInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<PlayerNickname>()
			         .AsSingle();

			Container.BindInterfacesAndSelfTo<SessionSystem>()
			         .AsSingle();
		}
	}
}