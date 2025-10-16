using System;
using System.Threading;
using System.Threading.Tasks;
using Game.App;
using Game.Network;
using R3;
using UnityEngine;
using Zenject;

namespace Game.UI
{
	public sealed class GameModePresenter : IInitializable, IDisposable
	{
		private readonly GameModeView _gameModeView;
		private readonly SessionSystem _sessionSystem;
		private readonly PlayerNickname _playerNickname;
		private readonly CompositeDisposable _disposable = new();

		public GameModePresenter(GameModeView gameModeView, SessionSystem sessionSystem, PlayerNickname playerNickname)
		{
			_gameModeView = gameModeView;
			_sessionSystem = sessionSystem;
			_playerNickname = playerNickname;
		}

		public void Initialize()
		{
			_gameModeView.Init();

			_gameModeView.OnHostGamePressed
			             .SubscribeAwait(OnHostGamePressed)
			             .AddTo(_disposable);

			_gameModeView.OnCopySessionIdPressed
			             .Subscribe(OnCopySessionIdPressed)
			             .AddTo(_disposable);

			_gameModeView.OnJoinGamePressed
			             .SubscribeAwait(OnJoinGamePressed)
			             .AddTo(_disposable);
		}

		private async ValueTask OnHostGamePressed(Unit arg1, CancellationToken arg2)
		{
			_gameModeView.SetHostButtonInteractable(false);

			try
			{
				var activeSession = _sessionSystem.ActiveSession;
				if (activeSession != null)
				{
					await activeSession.LeaveAsync();
				}

				await _sessionSystem.StartSessionAsHost(_playerNickname.Nickname);
				var newSession = _sessionSystem.ActiveSession;
				var code = newSession.Code;
				_gameModeView.HostSessionId = code;
			}
			catch (Exception e)
			{
				Debug.LogException(e);
			}
			finally
			{
				_gameModeView.SetHostButtonInteractable(true);
			}
		}

		private void OnCopySessionIdPressed(Unit _)
		{
			var session = _sessionSystem.ActiveSession;
			if (session != null)
			{
				CopyToClipboard(session.Code);
			}
		}

		private async ValueTask OnJoinGamePressed(Unit arg1, CancellationToken arg2)
		{
			if (string.IsNullOrEmpty(_gameModeView.JoinSessionId))
			{
				return;
			}

			try
			{
				_gameModeView.SetJoinButtonInteractable(false);
				await _sessionSystem.JoinSessionByCode(_gameModeView.JoinSessionId);
			}
			catch (Exception e)
			{
				Debug.LogException(e);
				_gameModeView.SetJoinButtonInteractable(true);
			}
		}

		private static void CopyToClipboard(string text)
		{
			GUIUtility.systemCopyBuffer = text;
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}