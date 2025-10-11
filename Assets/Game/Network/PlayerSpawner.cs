using Unity.Netcode;
using UnityEngine;

namespace Game.Network
{
	public class PlayerSpawner : MonoBehaviour
	{
		[SerializeField]
		private NetworkObject _hostPlayerPrefab;
		[SerializeField]
		private NetworkObject _clientPlayerPrefab;
		[SerializeField]
		private Transform _hostSpawnPoint;
		[SerializeField]
		private Transform _clientSpawnPoint;
		[SerializeField]
		private Transform _container;

		private void Start()
		{
			NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
		}

		private void OnClientConnected(ulong clientId)
		{
			if (NetworkManager.Singleton.IsServer == false)
			{
				return;
			}

			if (NetworkManager.Singleton.LocalClientId == clientId) // host
			{
				var player = Instantiate(_hostPlayerPrefab, _hostSpawnPoint.position, _hostSpawnPoint.rotation, _container);
				player.SpawnAsPlayerObject(clientId);
			}
			else
			{
				var player = Instantiate(_clientPlayerPrefab, _clientSpawnPoint.position, _clientSpawnPoint.rotation, _container);
				player.SpawnAsPlayerObject(clientId);
			}
		}

		private void OnDestroy()
		{
			if (NetworkManager.Singleton != null)
			{
				NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
			}
		}
	}
}