using System;
using Game.Network;
using Gameplay;
using R3;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.UI
{
	public sealed class GameOverPopupPresenter : IInitializable, IDisposable
	{
		private readonly GameFinisher _gameFinisher;
		private readonly MyPlayerService _myPlayerService;
		private readonly WinPopup _winPopup;
		private readonly LosePopup _losePopup;
		private readonly CompositeDisposable _disposable = new();

		public GameOverPopupPresenter(GameFinisher gameFinisher, MyPlayerService myPlayerService, WinPopup winPopup, LosePopup losePopup)
		{
			_gameFinisher = gameFinisher;
			_winPopup = winPopup;
			_losePopup = losePopup;
			_myPlayerService = myPlayerService;
		}

		public void Initialize()
		{
			_gameFinisher.OnPlayerWon
			             .Subscribe(OnPlayerWon)
			             .AddTo(_disposable);

			_winPopup.Init();
			_losePopup.Init();

			Observable.Merge(
				          _winPopup.OnBackButtonPressed,
				          _losePopup.OnBackButtonPressed)
			          .Subscribe(_ => SceneManager.LoadScene(Scenes.MainMenu.ToString()))
			          .AddTo(_disposable);
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