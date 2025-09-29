using Game.SceneSwitch;
using VContainer;
using VContainer.Unity;

namespace Installers
{
	public sealed class BootstrapInstaller : LifetimeScope
	{
		protected override void Configure(IContainerBuilder builder)
		{
			builder.Register<AuthSceneSwitchable>(Lifetime.Scoped)
			       .As<ISceneSwitchable>();
		}
	}
}