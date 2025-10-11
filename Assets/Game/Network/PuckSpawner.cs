using Sirenix.OdinInspector;
using Unity.Netcode;
using UnityEngine;

namespace Game.Network
{
	public sealed class PuckSpawner : MonoBehaviour
	{
		[SerializeField]
		private NetworkObject _puckPrefab;
		[SerializeField]
		private Transform _puckSpawnPoint;
		[SerializeField]
		private Transform _container;

		private void Start()
		{
			if (NetworkManager.Singleton == null)
			{
				UnityEngine.Debug.LogError("no manager yet");
			}

			NetworkManager.Singleton.OnServerStarted += OnServerStart;

			if (NetworkManager.Singleton.IsServer)
			{
				OnServerStart();
			}
		}

		private void OnServerStart()
		{
			SpawnPuck();
		}

		[Button]
		private void SpawnPuck()
		{
			var player = Instantiate(_puckPrefab, _puckSpawnPoint.position, _puckSpawnPoint.rotation, _container);
			player.Spawn();
		}

		private void OnDestroy()
		{
			NetworkManager.Singleton.OnServerStarted -= OnServerStart;
		}
	}
}