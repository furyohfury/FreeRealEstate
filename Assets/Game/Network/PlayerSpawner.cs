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
			NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += OnSceneLoaded;
		}

		private void OnSceneLoaded(string s, LoadSceneMode sceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
		{
			if (NetworkManager.Singleton.IsHost == false)
			{
				return;
			}

			foreach (var clientId in clientsCompleted)
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
				NetworkManager.Singleton.SceneManager.OnLoadEventCompleted -= OnSceneLoaded;
			}
		}
	}
}