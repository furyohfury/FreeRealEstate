using System.Collections.Generic;
using Gameplay;
using R3;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Network
{
	public class PlayerSpawner : MonoBehaviour
	{
		public Observable<Bat> OnHostSpawned => _onHostSpawned;
		public Observable<Bat> OnClientSpawned => _onClientSpawned;

		[SerializeField]
		private Bat _hostPlayerPrefab;
		[SerializeField]
		private Bat _clientPlayerPrefab;
		[SerializeField]
		private Transform _hostSpawnPoint;
		[SerializeField]
		private Transform _clientSpawnPoint;
		[SerializeField]
		private Transform _container;

		private readonly Subject<Bat> _onHostSpawned = new();
		private readonly Subject<Bat> _onClientSpawned = new();

		private void Start()
		{
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

		private void OnClientConnectedCallback(ulong clientId)
		{
			if (IsHost() == false)
			{
				return;
			}

			SpawnPlayer(clientId);
		}

		private void OnSceneLoaded(string s, LoadSceneMode sceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
		{
			if (IsHost() == false)
			{
				return;
			}

			foreach (var clientId in clientsCompleted)
			{
				SpawnPlayer(clientId);
			}
		}

		private static bool IsHost()
		{
			return NetworkManager.Singleton.IsHost;
		}

		private void SpawnPlayer(ulong clientId)
		{
			if (NetworkManager.Singleton.LocalClientId == clientId) // host
			{
				SpawnHostPlayer(clientId);
			}
			else
			{
				SpawnClientPlayer(clientId);
			}
		}

		private void SpawnHostPlayer(ulong clientId)
		{
			var player = Instantiate(_hostPlayerPrefab, _hostSpawnPoint.position, _hostSpawnPoint.rotation, _container);
			player.NetworkObject.SpawnAsPlayerObject(clientId);
			_onHostSpawned.OnNext(player);
		}

		private void SpawnClientPlayer(ulong clientId)
		{
			var player = Instantiate(_clientPlayerPrefab, _clientSpawnPoint.position, _clientSpawnPoint.rotation, _container);
			player.NetworkObject.SpawnAsPlayerObject(clientId);
			_onClientSpawned.OnNext(player);
		}

		private void OnDestroy()
		{
			if (NetworkManager.Singleton != null)
			{
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