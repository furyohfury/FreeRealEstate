using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game
{
	public sealed class SceneInstaller : MonoInstaller
	{
		[SerializeField]
		private InputActionReference _moveActionReference;
		[SerializeField]
		private Transform _playerPointer;

		public override void InstallBindings()
		{
			Container.Bind<Camera>()
			         .FromComponentInHierarchy()
			         .AsSingle();

			InstallPikminServices();
			InstallPlayerSceneSystems();
			InstallShipSceneSystems();
		}

		private void InstallPikminServices()
		{
			Container.BindInterfacesAndSelfTo<PikminService>()
			         .AsSingle();
			Container.BindInterfacesAndSelfTo<PikminDeathObserver>()
			         .AsSingle();
		}

		private void InstallPlayerSceneSystems()
		{
			Container.Bind<Player>()
			         .FromComponentInHierarchy()
			         .AsSingle();

			Container.Bind<PlayerPointer>()
			         .FromComponentInHierarchy()
			         .AsSingle();

			Container.Bind<PlayerService>()
			         .AsSingle();

			Container.BindInterfacesTo<PlayerController>()
			         .AsSingle();

			Container.BindInterfacesTo<PlayerRotationController>()
			         .AsSingle();

			Container.BindInterfacesTo<PlayerAnimatorController>()
			         .AsSingle();

			Container.Bind<InputControls>()
			         .AsSingle();

			Container.BindInterfacesAndSelfTo<PlayerInputReader>()
			         .AsSingle();
		}

		private void InstallShipSceneSystems()
		{
			Container.Bind<Ship>()
			         .FromComponentInHierarchy()
			         .AsSingle();

			Container.BindInterfacesTo<ShipConsumeObserver>()
			         .AsSingle();

			Container.BindInterfacesTo<ShipSpawnController>()
			         .AsSingle();
		}
	}
}