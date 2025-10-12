using Game;
using Game.Network;
using Gameplay;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Installers
{
	public sealed class GameplayCoreInstaller : MonoInstaller
	{
		[SerializeField] [Required]
		private Transform _hostPlayerSpawnPoint;
		[SerializeField] [Required]
		private Transform _clientPlayerSpawnPoint;
		[SerializeField] [Required]
		private Transform _hostPuckSpawnPoint;
		[SerializeField] [Required]
		private Transform _clientPuckSpawnPoint;

		public override void InstallBindings()
		{
			Container.Bind<PlayerSpawner>()
			         .FromComponentInHierarchy()
			         .AsSingle();

			Container.Bind<PuckSpawner>()
			         .FromComponentInHierarchy()
			         .AsSingle();

			Container.Bind<HostPlayerService>()
			         .AsSingle();

			Container.Bind<ClientPlayerService>()
			         .AsSingle();

			Container.Bind<PuckService>()
			         .AsSingle();

			Container.Bind<RoundRestarter>()
			         .AsSingle();

			Container.Bind<PlayersSpawnService>()
			         .FromInstance(new PlayersSpawnService(_hostPlayerSpawnPoint, _clientPlayerSpawnPoint))
			         .AsSingle();

			Container.Bind<PuckSpawnPositionService>()
			         .FromInstance(new PuckSpawnPositionService(_hostPuckSpawnPoint, _clientPuckSpawnPoint))
			         .AsSingle();

			Container.BindInterfacesTo<PlayerSpawnObserver>()
			         .AsSingle();

			Container.BindInterfacesTo<PuckSpawnObserver>()
			         .AsSingle();
		}
	}
}