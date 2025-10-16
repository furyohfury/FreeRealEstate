using Gameplay;
using R3;
using Sirenix.OdinInspector;
using Unity.Netcode;
using UnityEngine;

namespace Game.Network
{
	public sealed class PuckSpawner : MonoBehaviour
	{
		public Observable<Puck> OnPuckSpawned => _onPuckSpawned;

		[SerializeField]
		private Puck _puckPrefab;
		[SerializeField]
		private Transform _puckSpawnPoint;
		[SerializeField]
		private Transform _container;

		private readonly Subject<Puck> _onPuckSpawned = new();

		private void Start()
		{
			if (NetworkManager.Singleton == null)
			{
				Debug.LogError("no manager yet");
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
			var puck = Instantiate(_puckPrefab, _puckSpawnPoint.position, _puckSpawnPoint.rotation, _container);
			puck.GetComponent<NetworkObject>().Spawn();
			_onPuckSpawned.OnNext(puck);
		}

		private void OnDestroy()
		{
			if (NetworkManager.Singleton != null)
			{
				NetworkManager.Singleton.OnServerStarted -= OnServerStart;
			}
		}
	}
}