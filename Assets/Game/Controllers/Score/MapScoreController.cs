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
		private readonly SerialDisposable _disposable = new();

		public MapScoreController(MapScore mapScore, ElementsClickHandler elementsClickHandler, PointsForStatusConfig config)
		{
			_mapScore = mapScore;
			_elementsClickHandler = elementsClickHandler;
			_config = config;
		}

		public void Start()
		{
			_disposable.Disposable = _elementsClickHandler.OnClickHandled
			                                              .Subscribe(OnPointsAdded);
		}

		private void OnPointsAdded(ClickStatus status)
		{
			_mapScore.AddPoints(_config.Points[status]);
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}