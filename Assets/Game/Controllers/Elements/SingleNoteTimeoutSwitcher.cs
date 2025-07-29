using System;
using System.Linq;
using Beatmaps;
using Game.BeatmapControl;
using Game.SongMapTime;
using R3;
using VContainer.Unity;

namespace Game
{
	public sealed class SingleNoteTimeoutSwitcher : IStartable, ITickable, IDisposable
	{
		private BeatmapPipeline _beatmapPipeline;
		private readonly IMapTime _mapTime;
		private float _clickInterval;

		public SingleNoteTimeoutSwitcher(BeatmapPipeline beatmapPipeline, IMapTime mapTime)
		{
			_beatmapPipeline = beatmapPipeline;
			_mapTime = mapTime;
		}

		private readonly SerialDisposable _disposable = new();

		public void Start()
		{
			_disposable.Disposable = _beatmapPipeline.Map
			                                         .Where(map => map != null)
			                                         .Subscribe(map =>
			                                         {
				                                         _clickInterval = map
				                                                          .GetDifficulty()
				                                                          .GetDifficultyParams()
				                                                          .OfType<ClickIntervalParams>()
				                                                          .Single()
				                                                          .GetClickInterval();
			                                         });
		}

		public void Tick()
		{
			var element = _beatmapPipeline.Element.CurrentValue;
			if (element is not SingleNote)
			{
				return;
			}

			var elementTimeSeconds = element.HitTime;
			var mapTime = _mapTime.GetMapTimeInSeconds();
			if (IsBeyondClickInterval(mapTime, elementTimeSeconds))
			{
				_beatmapPipeline.SwitchToNextElement();
			}
		}

		private bool IsBeyondClickInterval(float mapTime, float elementTimeSeconds)
		{
			return mapTime - elementTimeSeconds > _clickInterval;
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}