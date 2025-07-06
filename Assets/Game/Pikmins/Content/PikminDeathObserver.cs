using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using Zenject;

namespace Game
{
	public sealed class PikminDeathObserver : IInitializable, IDisposable
	{
		private readonly PikminService _pikminService;
		private readonly Ship _ship;

		private readonly float _delayBetweenSpawns = 0.5f;
		private readonly SemaphoreSlim _semaphore = new(1, 1);
		private readonly CompositeDisposable _disposable = new();

		public PikminDeathObserver(PikminService pikminService, Ship ship)
		{
			_pikminService = pikminService;
			_ship = ship;
		}

		public void Initialize()
		{
			_pikminService.PikminCount
			              .Pairwise()
			              .Where(pair => pair.Current < pair.Previous)
			              .Subscribe(_ => ReplaceDeadPikmins().Forget())
			              .AddTo(_disposable);
		}

		private async UniTask ReplaceDeadPikmins()
		{
			await _semaphore.WaitAsync();
			while (_pikminService.CurrentCount < PikminService.CurrentMaxCount)
			{
				SpawnEntity();
				await UniTask.Delay(TimeSpan.FromSeconds(_delayBetweenSpawns));
			}

			_semaphore.Release();
		}

		private void SpawnEntity()
		{
			var newPikmin = _ship.CreateEntity();
			_pikminService.AddPikmin(newPikmin);
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}