using System;
using Game.Network;
using R3;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.UI
{
	public sealed class GameOverPopupPresenter : IInitializable, IDisposable
	{
		private readonly WinPopup _winPopup;
		private readonly LosePopup _losePopup;
		private IDisposable _disposable;

		public GameOverPopupPresenter(WinPopup winPopup, LosePopup losePopup)
		{
			_winPopup = winPopup;
			_losePopup = losePopup;
		}

		public void Initialize()
		{
			_winPopup.Init();
			_losePopup.Init();

			_disposable = Observable.Merge(
				                        _winPopup.OnBackButtonPressed,
				                        _losePopup.OnBackButtonPressed)
			                        .Subscribe(_ => SceneManager.LoadScene(Scenes.MainMenu.ToString()));
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}