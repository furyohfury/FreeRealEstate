using Game.UI;
using Zenject;

namespace Installers
{
	public sealed class EnterNameMenuInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<EnterNameView>()
			         .FromComponentInHierarchy()
			         .AsSingle();

			Container.BindInterfacesTo<EnterNamePresenter>()
			         .AsSingle();
		}
	}
}