using System;
using GameEngine;
using R3;
using UnityEngine;
using Zenject;

namespace Game
{
	public class GameOverObserver : IInitializable, IDisposable
	{
		private readonly GameObject _finalCarriable;
		private readonly GameOverScreen _gameOverScreen;
		private readonly CompositeDisposable _disposable = new();

		public GameOverObserver(GameObject finalCarriable, GameOverScreen gameOverScreen)
		{
			_finalCarriable = finalCarriable;
			_gameOverScreen = gameOverScreen;
		}

		public void Initialize()
		{
			if (_finalCarriable.TryGetComponent(out IDestroyable destroyable) == false)
			{
				throw new ArgumentException();
			}

			destroyable.OnDead
			           .Subscribe(OnGameOver)
			           .AddTo(_disposable);
		}

		private void OnGameOver(GameObject lastObject)
		{
			Time.timeScale = 0f;
			_gameOverScreen.Show();
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}