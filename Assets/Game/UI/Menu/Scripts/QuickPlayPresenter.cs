using System;
using System.Threading;
using System.Threading.Tasks;
using Game.App;
using Game.Network;
using R3;
using R3.Triggers;
using Unity.Services.Multiplayer;
using Zenject;

namespace Game.UI
{
	public sealed class QuickPlayPresenter : IInitializable, IDisposable
	{
		private readonly QuickPlayMenu _quickPlayMenu;
		private readonly SessionSystem _sessionSystem;
		private readonly PlayerNickname _playerNickname;
		private readonly GameModeView _gameModeView;

		private const string NO_LOBBIES_TEXT = "No lobbies found.";
		private const string AWAITING_PLAYERS_TEXT = "Created lobby. Awaiting players...";
		private readonly CompositeDisposable _disposable = new();

		public QuickPlayPresenter(QuickPlayMenu quickPlayMenu, SessionSystem sessionSystem, PlayerNickname playerNickname, GameModeView gameModeView)
		{
			_quickPlayMenu = quickPlayMenu;
			_sessionSystem = sessionSystem;
			_playerNickname = playerNickname;
			_gameModeView = gameModeView;
		}

		public void Initialize()
		{
			_quickPlayMenu.Init();

			_quickPlayMenu.OnEnableAsObservable()
			              .SubscribeAwait(OnQuickPlayMenuEnabled)
			              .AddTo(_disposable);

			_quickPlayMenu.OnHostButtonPressed
			              .SubscribeAwait(OnHostButtonPressed)
			              .AddTo(_disposable);

			_quickPlayMenu.OnBackButtonPressed
			              .SubscribeAwait(OnBackButtonPressed)
			              .AddTo(_disposable);
		}

		private async ValueTask OnQuickPlayMenuEnabled(Unit arg1, CancellationToken arg2)
		{
			DisableButtons();

			try
			{
				await _sessionSystem.QuickPlay();
			}
			catch (SessionException)
			{
				if (_sessionSystem.ActiveSession == null)
				{
					_quickPlayMenu.HideLoadingUIAnimation();
					_quickPlayMenu.GeneralMessageText = NO_LOBBIES_TEXT;
					_quickPlayMenu.ShowHostButton();
					_quickPlayMenu.ShowBackButton();
				}
			}
			finally
			{
				EnableButtons();
			}
		}

		private async ValueTask OnHostButtonPressed(Unit arg1, CancellationToken arg2)
		{
			DisableButtons();

			try
			{
				await _sessionSystem.HostPublicSession(_playerNickname.Nickname);
				_quickPlayMenu.ShowLoadingUIAnimation();
				_quickPlayMenu.GeneralMessageText = AWAITING_PLAYERS_TEXT;
			}
			catch (Exception e)
			{
				_quickPlayMenu.GeneralMessageText = e.Message;
			}
			finally
			{
				EnableButtons();
			}
		}

		private void EnableButtons()
		{
			_quickPlayMenu.SetHostButtonInteractable(true);
			_quickPlayMenu.SetBackButtonInteractable(true);
		}

		private void DisableButtons()
		{
			_quickPlayMenu.SetHostButtonInteractable(false);
			_quickPlayMenu.SetBackButtonInteractable(false);
		}

		private async ValueTask OnBackButtonPressed(Unit arg1, CancellationToken arg2)
		{
			await _sessionSystem.LeaveCurrentSession();
			_quickPlayMenu.Hide();
			_gameModeView.Show();
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}