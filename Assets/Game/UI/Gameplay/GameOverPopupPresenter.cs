using System;
using System.Threading;
using System.Threading.Tasks;
using Game.Network;
using Gameplay;
using R3;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.UI
{
	public sealed class GameOverPopupPresenter : IInitializable, IDisposable
	{
		private readonly MatchSystem _matchSystem;
		private readonly SessionSystem _sessionSystem;
		private readonly MyPlayerService _myPlayerService;
		private readonly WinPopup _winPopup;
		private readonly LosePopup _losePopup;
		private readonly CompositeDisposable _disposable = new();

		public GameOverPopupPresenter(
			MatchSystem matchSystem,
			MyPlayerService myPlayerService,
			WinPopup winPopup,
			LosePopup losePopup,
			SessionSystem sessionSystem
			)
		{
			_matchSystem = matchSystem;
			_winPopup = winPopup;
			_losePopup = losePopup;
			_sessionSystem = sessionSystem;
			_myPlayerService = myPlayerService;
		}

		public void Initialize()
		{
			_matchSystem.OnMatchWon
			            .Subscribe(OnPlayerWon)
			            .AddTo(_disposable);

			_winPopup.Init();
			_losePopup.Init();

			Observable.Merge(
				          _winPopup.OnBackButtonPressed,
				          _losePopup.OnBackButtonPressed)
			          .SubscribeAwait(OnBackButtonPressed)
			          .AddTo(_disposable);
		}

		private async ValueTask OnBackButtonPressed(Unit _, CancellationToken cancellationToken)
		{
			SceneManager.LoadScene(Scenes.MainMenu.ToString());
			await _sessionSystem.LeaveCurrentSession();
		}

		private void OnPlayerWon(Player player)
		{
			if (_myPlayerService.MyPlayer == player)
			{
				_winPopup.Show();
			}
			else
			{
				_losePopup.Show();
			}
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}