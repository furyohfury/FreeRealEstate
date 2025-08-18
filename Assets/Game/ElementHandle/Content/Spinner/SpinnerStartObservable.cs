using System;
using Beatmaps;
using Game.BeatmapControl;
using Game.BeatmapTime;
using R3;
using VContainer.Unity;

namespace Game.ElementHandle
{
	public sealed class SpinnerStartObservable : IElementHandleEmitter, IInitializable
	{
		private readonly BeatmapPipeline _beatmapPipeline;
		private readonly IMapTime _mapTime;

		private Observable<HandleResult> _observable;

		public SpinnerStartObservable(BeatmapPipeline beatmapPipeline, IMapTime mapTime)
		{
			_beatmapPipeline = beatmapPipeline;
			_mapTime = mapTime;
		}

		public void Initialize()
		{
			_observable = _beatmapPipeline.Element
			                              .OfType<MapElement, Spinner>()
			                              .SelectMany(spinner =>
				                              Observable.Timer(
					                                        TimeSpan.FromSeconds(spinner.HitTime - _mapTime.GetMapTimeInSeconds())
				                                        )
				                                        .Select(_ => (HandleResult)new SpinnerStartedHandleResult(spinner))
			                              );
		}

		public Observable<HandleResult> GetStream()
		{
			return _observable;
		}
	}
}