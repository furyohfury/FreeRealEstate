using System;
using Game.ElementHandle;
using Game.Scoring;
using R3;
using VContainer.Unity;

namespace Game
{
	public sealed class MapScoreController : IStartable, IDisposable
	{
		private readonly MapScore _mapScore;
		private readonly ElementsClickHandler _elementsClickHandler;
		private readonly PointsForStatusConfig _config;
		private readonly CompositeDisposable _disposable = new();

		public MapScoreController(MapScore mapScore, ElementsClickHandler elementsClickHandler, PointsForStatusConfig config)
		{
			_mapScore = mapScore;
			_elementsClickHandler = elementsClickHandler;
			_config = config;
		}

		public void Start()
		{
			Observable.FromEvent<ClickStatus>(
				          h => _elementsClickHandler.OnElementHandled += h,
				          h => _elementsClickHandler.OnElementHandled -= h)
			          .Subscribe(status => _mapScore.AddPoints(_config.Points[status]))
			          .AddTo(_disposable);
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}