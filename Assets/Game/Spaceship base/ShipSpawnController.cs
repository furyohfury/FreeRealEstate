using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using Zenject;

namespace Game
{
	public sealed class ShipSpawnController : IInitializable, IDisposable
	{
		private readonly Ship _ship;
		private readonly ShipPoints _shipPoints;
		private readonly PikminService _pikminService;
		private readonly float _delayBetweenSpawns = 0.5f; // TODO to config

		private readonly CompositeDisposable _disposable = new();
		private readonly SemaphoreSlim _semaphore = new(1, 1);

		public ShipSpawnController(Ship ship, ShipPoints shipPoints, PikminService pikminService)
		{
			_ship = ship;
			_shipPoints = shipPoints;
			_pikminService = pikminService;
		}

		public void Initialize()
		{
			_shipPoints.Points
			           .Pairwise()
			           .Where(pair => pair.Previous < pair.Current)
			           .Subscribe(_ => OnPointGained().Forget())
			           .AddTo(_disposable);
		}

		private async UniTask OnPointGained()
		{
			await _semaphore.WaitAsync();
			ReactiveProperty<int> points = _shipPoints.Points;
			while (_pikminService.CurrentCount < PikminService.TOTAL_MAX_COUNT
			       && points.CurrentValue > 0)
			{
				SpawnEntity();
				await UniTask.Delay(TimeSpan.FromSeconds(_delayBetweenSpawns));
				points.Value -= 1; // TODO delete magic number 
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
			_disposable.Clear();
		}
	}
}