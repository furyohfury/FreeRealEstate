using UnityEngine;

namespace Gameplay
{
	public sealed class PuckSpawnPositionService
	{
		public Transform HostSidePosition => _hostSidePosition;
		public Transform ClientSidePosition => _clientSidePosition;

		private readonly Transform _clientSidePosition;
		private readonly Transform _hostSidePosition;

		public PuckSpawnPositionService(Transform hostSidePosition, Transform clientSidePosition)
		{
			_hostSidePosition = hostSidePosition;
			_clientSidePosition = clientSidePosition;
		}
	}
}