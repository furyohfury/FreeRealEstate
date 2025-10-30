using R3;
using Unity.Netcode;
using UnityEngine;

namespace Gameplay
{
	public sealed class GameFinisher
	{
		public Observable<Player> OnPlayerWon => _onPlayerWon;
		private readonly Subject<Player> _onPlayerWon = new();

		private readonly HostPlayerService _hostPlayerService;
		private readonly ClientPlayerService _clientPlayerService;

		public GameFinisher(HostPlayerService hostPlayerService, ClientPlayerService clientPlayerService)
		{
			_hostPlayerService = hostPlayerService;
			_clientPlayerService = clientPlayerService;
		}

		public void FinishGameByPlayerWon(Player wonPlayer)
		{
			if (NetworkManager.Singleton.IsHost)
			{
				_hostPlayerService.HostPlayer.DisableMovement();
				_clientPlayerService.ClientPlayer.DisableMovement();
			}

			Debug.Log($"<color=yellow> Player {wonPlayer.ToString()} won!</color>");

			_onPlayerWon.OnNext(wonPlayer);
		}
	}
}