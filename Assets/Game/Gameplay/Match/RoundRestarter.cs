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

		public void RestartByGoalHit(Player player)
		{
			if (NetworkManager.Singleton.IsHost == false)
			{
				return;
			}
			
			player = player == Player.One
				? Player.Two
				: Player.One;
			
			RestartToSide(player);
		}

		private void RestartToSide(Player player)
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

		private void RestartInHostSide()
		{
			MovePlayersToDefaultPos();
			SpawnPuckInHostPos();
		}

		private void RestartInClientSide()
		{
			MovePlayersToDefaultPos();
			SpawnPuckInClientPos();
		}

		private void MovePlayersToDefaultPos()
		{
			var host = _hostPlayerService.HostPlayer;
			host.MoveTo(_playersSpawnService.HostSpawnPos.position);
			var client = _clientPlayerService.ClientPlayer;
			client.MoveTo(_playersSpawnService.ClientSpawnPos.position);
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