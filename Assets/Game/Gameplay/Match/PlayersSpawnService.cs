using UnityEngine;

namespace Gameplay
{
	public sealed class PlayersSpawnService
	{
		public Transform HostSpawnPos => _hostSpawnPos;
		public Transform ClientSpawnPos => _clientSpawnPos;

		private readonly Transform _clientSpawnPos;
		private readonly Transform _hostSpawnPos;

		public PlayersSpawnService(Transform hostSpawnPos, Transform clientSpawnPos)
		{
			_hostSpawnPos = hostSpawnPos;
			_clientSpawnPos = clientSpawnPos;
		}
	}
}