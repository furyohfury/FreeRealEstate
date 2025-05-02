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
			Container.Bind<PlayerService>()
			         .AsSingle()
			         .WithArguments(_player);

			Container.BindInterfacesTo<PlayerMoveController>()
			         .AsSingle();

			Container.Bind<InputControls>()
			         .AsSingle();

			Container.BindInterfacesAndSelfTo<PlayerInputReader>()
			         .AsSingle();
		}
	}
}