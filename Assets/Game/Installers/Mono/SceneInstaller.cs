using GameEngine;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game
{
	public sealed class SceneInstaller : MonoInstaller
	{
		[SerializeField]
		private Player _player;
		[SerializeField]
		private InputActionReference _moveActionReference;
		
		public override void InstallBindings()
		{
			InstallPlayerSceneSystems();
			InstallShipSceneSystems();
		}

		private void InstallPlayerSceneSystems()
		{
			Container.Bind<PlayerService>()
			         .AsSingle()
			         .WithArguments(_player);

			Container.BindInterfacesTo<PlayerController>()
			         .AsSingle();

			Container.Bind<InputControls>()
			         .AsSingle();

			Container.BindInterfacesAndSelfTo<PlayerInputReader>()
			         .AsSingle();
		}

		private void InstallShipSceneSystems()
		{
			Container.Bind<Entity>()
			         .To<Ship>()
			         .FromComponentInHierarchy()
			         .AsSingle();
			
			Container.BindInterfacesTo<ShipConsumeObserver>()
			         .AsSingle();
			
			Container.BindInterfacesTo<ShipSpawnController>()
			         .AsSingle();

			Container.BindInterfacesTo<ShipDetectObjectObserver>()
			         .AsSingle();
		}
	}
}