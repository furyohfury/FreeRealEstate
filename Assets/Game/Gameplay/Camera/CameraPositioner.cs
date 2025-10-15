using System;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Camera
{
	public sealed class CameraPositioner : IInitializable, IDisposable
	{
		private readonly UnityEngine.Camera _camera;
		private readonly Transform _playerOnePosition;
		private readonly Transform _playerTwoPosition;

		public CameraPositioner(UnityEngine.Camera camera, Transform playerOnePosition, Transform playerTwoPosition)
		{
			_camera = camera;
			_playerOnePosition = playerOnePosition;
			_playerTwoPosition = playerTwoPosition;
		}

		public void Initialize()
		{
			NetworkManager.Singleton.OnClientStarted += OnClientStarted;
		}

		private void OnClientStarted()
		{
			SetCameraPosition(NetworkManager.Singleton.IsHost
				? _playerOnePosition
				: _playerTwoPosition);
		}

		private void SetCameraPosition(Transform position)
		{
			_camera.transform.SetParent(position);
			_camera.transform.localPosition = Vector3.zero;
			_camera.transform.localRotation = Quaternion.identity;
		}

		public void Dispose()
		{
			NetworkManager.Singleton.OnClientStarted -= OnClientStarted;
		}
	}
}