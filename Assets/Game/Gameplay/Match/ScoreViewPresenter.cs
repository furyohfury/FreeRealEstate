using System;
using Game.App;
using Game.Network;
using R3;
using Unity.Netcode;
using Unity.Services.Multiplayer;
using Zenject;

namespace Gameplay
{
	public sealed class ScoreViewPresenter : IInitializable, IDisposable
	{
		private readonly ScoreView _scoreView;
		private readonly SessionSystem _sessionSystem;
		private readonly MyPlayerService _myPlayerService;
		private readonly PlayerNickname _playerNickname;

		private IDisposable _disposable;

		public ScoreViewPresenter(ScoreView scoreView, SessionSystem sessionSystem, MyPlayerService myPlayerService, PlayerNickname playerNickname)
		{
			_scoreView = scoreView;
			_sessionSystem = sessionSystem;
			_myPlayerService = myPlayerService;
			_playerNickname = playerNickname;
		}

		public void Initialize()
		{
			var activeSession = _sessionSystem.ActiveSession;

			if (activeSession == null)
			{
				_disposable = _sessionSystem.OnSessionStarted
				                            .Subscribe(OnClientConnectedCallback);
			}
			else
			{
				FillNames(activeSession);
			}
		}

		private void FillNames(ISession activeSession)
		{
			var currentPlayerNickname = _playerNickname.Nickname;
			string otherPlayerNickname = null;

			var playersData = activeSession.Players;

			for (int i = 0, count = playersData.Count; i < count; i++)
			{
				if (playersData[i] == activeSession.CurrentPlayer)
				{
					continue;
				}

				otherPlayerNickname = playersData[i].Properties[SessionStaticData.PLAYER_NAME_PROPERTY_KEY].Value;
			}

			if (_myPlayerService.MyPlayer is Player.One)
			{
				_scoreView.SetP1Name(currentPlayerNickname);
				_scoreView.SetP2Name(otherPlayerNickname);
			}
			else
			{
				_scoreView.SetP2Name(currentPlayerNickname);
				_scoreView.SetP1Name(otherPlayerNickname);
			}
		}

		private void OnClientConnectedCallback(ISession session)
		{
			FillNames(session);
		}

		public void Dispose()
		{
			if (NetworkManager.Singleton != null)
			{
				_disposable.Dispose();
			}
		}
	}
}