using System;
using Beatmaps;
using Game.BeatmapControl;
using Game.BeatmapTime;
using R3;
using VContainer.Unity;

namespace Game
{
	public sealed class ElementTimeoutObservable : IElementTimeoutObservable, IInitializable, IDisposable
	{
		public Observable<MapElement> OnTimeout => _onTimeout;

		private readonly Subject<MapElement> _onTimeout = new();
		private readonly ElementTimeoutHelper _elementTimeoutHelper;
		private readonly BeatmapPipeline _beatmapPipeline;
		private readonly IMapTime _mapTime;

		private IDisposable _disposable;

		public ElementTimeoutObservable(ElementTimeoutHelper elementTimeoutHelper, BeatmapPipeline beatmapPipeline, IMapTime mapTime)
		{
			_elementTimeoutHelper = elementTimeoutHelper;
			_beatmapPipeline = beatmapPipeline;
			_mapTime = mapTime;
		}

		public void Initialize()
		{
			_disposable = Observable.EveryUpdate()
			                        .Where(_ => _beatmapPipeline.Map.CurrentValue != null && _beatmapPipeline.IsEnded == false)
			                        .WithLatestFrom(_beatmapPipeline.Element, (_, element) => element)
			                        .Subscribe(element =>
			                        {
				                        var currentBeatmap = _beatmapPipeline.Map.CurrentValue;
				                        var difficulty = currentBeatmap.GetDifficulty();
				                        var timeout = _elementTimeoutHelper.GetTimeout(element, difficulty);

				                        var mapTime = _mapTime.GetMapTimeInSeconds();
				                        if (IsBeyondMissWindow(mapTime, timeout))
				                        {
					                        _onTimeout.OnNext(element);
				                        }
			                        });
		}

		private bool IsBeyondMissWindow(float mapTime, float timeout)
		{
			return mapTime > timeout;
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}