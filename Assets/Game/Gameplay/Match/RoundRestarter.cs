using Unity.Netcode;

namespace Gameplay
{
	public sealed class RoundRestarter
	{
		private readonly HostPlayerService _hostPlayerService;
		private readonly ClientPlayerService _clientPlayerService;
		private readonly PlayersSpawnService _playersSpawnService;
		private readonly PuckService _puckService;
		private readonly PuckSpawnPositionService _puckSpawnPositionService;

		public RoundRestarter(PlayersSpawnService playersSpawnService, HostPlayerService hostPlayerService
			, ClientPlayerService clientPlayerService, PuckSpawnPositionService puckSpawnPositionService, PuckService puckService)
		{
			_playersSpawnService = playersSpawnService;
			_hostPlayerService = hostPlayerService;
			_clientPlayerService = clientPlayerService;
			_puckSpawnPositionService = puckSpawnPositionService;
			_puckService = puckService;
		}

		public void RestartToSide(Player player)
		{
			if (player == Player.One)
			{
				RestartInHostSide();
			}
			else
			{
				RestartInClientSide();
			}
		}

		public void RestartByGoalHit(Player player)
		{
			player = player == Player.One
				? Player.Two
				: Player.One;
			RestartToSide(player);
		}

		public void RestartInHostSide()
		{
				MovePlayersToDefaultPos();
				SpawnPuckInHostPos();
		}

		public void RestartInClientSide()
		{
			MovePlayersToDefaultPos();
			SpawnPuckInClientPos();
		}

		private void MovePlayersToDefaultPos()
		{
			var host = _hostPlayerService.HostPlayer;
			host.transform.position = _playersSpawnService.HostSpawnPos.position;
			host.GetComponent<MoveComponent>().MoveTo(host.transform.position);
			var client = _clientPlayerService.ClientPlayer;
			client.transform.position = _playersSpawnService.ClientSpawnPos.position;
			client.GetComponent<MoveComponent>().MoveTo(client.transform.position);
		}

		private void SpawnPuckInHostPos()
		{
			var puck = _puckService.Puck;
			puck.Move(_puckSpawnPositionService.HostSidePosition.position);
			puck.StopMovement();
		}

		private void SpawnPuckInClientPos()
		{
			var puck = _puckService.Puck;
			puck.Move(_puckSpawnPositionService.ClientSidePosition.position);
			puck.StopMovement();
		}
	}
}