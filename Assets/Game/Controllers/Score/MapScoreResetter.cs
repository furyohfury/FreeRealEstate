using System;
using Game.Scoring;
using R3;
using VContainer.Unity;

namespace Game
{
	public sealed class MapScoreResetter : IStartable, IDisposable
	{
		private readonly MapScore _mapScore;
		private ActiveMapService _activeMapService;
		private readonly CompositeDisposable _disposable = new();

		public MapScoreResetter(MapScore mapScore, ActiveMapService activeMapService)
		{
			_mapScore = mapScore;
			_activeMapService = activeMapService;
		}

		public void Start()
		{
			_activeMapService.ActiveMap
			                 .Subscribe(_ => _mapScore.Reset())
			                 .AddTo(_disposable);
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}