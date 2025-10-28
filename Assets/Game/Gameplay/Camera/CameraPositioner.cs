using System;
using System.Collections.Generic;
using R3;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Gameplay
{
	public sealed class CameraPositioner : IInitializable, IDisposable
	{
		private readonly Camera _camera;
		private readonly Transform _playerOnePosition;
		private readonly Transform _playerTwoPosition;

		public CameraPositioner(Camera camera, Transform playerOnePosition, Transform playerTwoPosition)
		{
			_camera = camera;
			_playerOnePosition = playerOnePosition;
			_playerTwoPosition = playerTwoPosition;
		}

		public void Initialize()
		{
			// NetworkManager.Singleton.OnClientStarted += OnClientStarted;
			var sceneManager = NetworkManager.Singleton.SceneManager;
			if (sceneManager != null)
			{
				sceneManager.OnLoadEventCompleted += OnSceneLoaded;
			}
			else
			{
				NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
			}
		}

		private void OnClientConnectedCallback(ulong obj)
		{
			SetCameraPosition();
		}

		private void OnSceneLoaded(string scenename, LoadSceneMode loadscenemode, List<ulong> clientscompleted, List<ulong> clientstimedout)
		{
			SetCameraPosition();
		}

		private void OnClientStarted()
		{
			SetCameraPosition();
		}

		private void SetCameraPosition()
		{
			var position = NetworkManager.Singleton.IsHost
				? _playerOnePosition
				: _playerTwoPosition;
			
			_camera.transform.SetParent(position);
			_camera.transform.localPosition = Vector3.zero;
			_camera.transform.localRotation = Quaternion.identity;
		}

		public void Dispose()
		{
			if (NetworkManager.Singleton != null)
			{
				// NetworkManager.Singleton.OnClientStarted -= OnClientStarted;
				var sceneManager = NetworkManager.Singleton.SceneManager;
				if (sceneManager != null)
				{
					sceneManager.OnLoadEventCompleted -= OnSceneLoaded;
				}
				else
				{
					NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnectedCallback;
				}
			}
		}
	}
}