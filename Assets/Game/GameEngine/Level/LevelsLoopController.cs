using System;
using R3;
using Zenject;

namespace Game
{
	public sealed class LevelsLoopController : IInitializable, IDisposable
	{
		private readonly ActiveLevelService _activeLevelService;

		private readonly CompositeDisposable _disposable = new();

		public LevelsLoopController(ActiveLevelService activeLevelService)
		{
			_activeLevelService = activeLevelService;
		}

		public void Initialize()
		{
			_activeLevelService.OnLevelEnded
			                   .Subscribe(_ => OnLevelEnded())
			                   .AddTo(_disposable);
		}

		private void OnLevelEnded()
		{
			LoopLevel();
		}

		private void LoopLevel()
		{
			_activeLevelService.Reset();
		}

		public void Dispose()
		{
			_disposable.Clear();
		}
	}
}