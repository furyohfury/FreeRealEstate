using Cysharp.Threading.Tasks;
using Game.Network;
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
		private SessionSystem _sessionSystem;

		public GameFinisher(HostPlayerService hostPlayerService, ClientPlayerService clientPlayerService, SessionSystem sessionSystem)
		{
			_hostPlayerService = hostPlayerService;
			_clientPlayerService = clientPlayerService;
			_sessionSystem = sessionSystem;
		}

		public async UniTask FinishGameByPlayerWon(Player wonPlayer)
		{
			if (NetworkManager.Singleton.IsHost)
			{
				_hostPlayerService.HostPlayer.DisableMovement();
				_clientPlayerService.ClientPlayer.DisableMovement();
			}

			Debug.Log($"<color=yellow> Player {wonPlayer.ToString()} won!</color>");
			_onPlayerWon.OnNext(wonPlayer);
			
			await _sessionSystem.LeaveCurrentSession();
		}
	}
}