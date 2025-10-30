using Game.UI;
using Zenject;

namespace Installers
{
	public sealed class QuickPlayMenuInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<QuickPlayMenu>()
			         .FromComponentInHierarchy()
			         .AsSingle();

			Container.BindInterfacesTo<QuickPlayPresenter>()
			         .AsSingle();
		}
	}
}