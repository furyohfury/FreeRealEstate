using System;
using R3;
using Zenject;

namespace Game
{
	public sealed class LevelsLoopController : IInitializable, IDisposable
	{
		private readonly LevelCellsSwitcher _levelCellsSwitcher;

		private readonly CompositeDisposable _disposable = new();

		public LevelsLoopController(LevelCellsSwitcher levelCellsSwitcher)
		{
			_levelCellsSwitcher = levelCellsSwitcher;
		}

		public void Initialize()
		{
			_levelCellsSwitcher.OnLevelEnded
			                   .Subscribe(_ => OnLevelEnded())
			                   .AddTo(_disposable);
		}

		private void OnLevelEnded()
		{
			LoopLevel();
		}

		private void LoopLevel()
		{
			_levelCellsSwitcher.Reset();
		}

		public void Dispose()
		{
			_disposable.Clear();
		}
	}
}