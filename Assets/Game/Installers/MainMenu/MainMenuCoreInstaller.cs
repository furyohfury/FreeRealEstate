using Game.Network;
using UnityEngine;
using Zenject;

namespace Installers
{
	[CreateAssetMenu(
		fileName = nameof(MainMenuCoreInstaller),
		menuName = nameof(Installers) + "/" + nameof(MainMenuCoreInstaller))]
	public sealed class MainMenuCoreInstaller : ScriptableObjectInstaller
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesTo<LobbyLaunchSub>()
			         .AsSingle();
		}
	}
}