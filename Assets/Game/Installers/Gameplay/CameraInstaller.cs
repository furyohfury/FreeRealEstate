using Game.Gameplay;
using UnityEngine;
using Zenject;

namespace Installers
{
	public sealed class CameraInstaller : MonoInstaller
	{
		[SerializeField]
		private Transform _playerOneCameraPos;
		[SerializeField]
		private Transform _playerTwoCameraPos;

		public override void InstallBindings()
		{
			Container.Bind<Camera>()
			         .FromInstance(Camera.main)
			         .AsSingle();

			Container.BindInterfacesTo<CameraPositioner>()
			         .FromInstance(new CameraPositioner(Camera.main, _playerOneCameraPos, _playerTwoCameraPos))
			         .AsSingle();
		}
	}
}