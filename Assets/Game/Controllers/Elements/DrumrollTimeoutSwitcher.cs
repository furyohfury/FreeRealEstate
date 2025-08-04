using System;
using System.Linq;
using Beatmaps;
using Game.BeatmapControl;
using Game.SongMapTime;
using R3;
using VContainer.Unity;

namespace Game
{
	public sealed class DrumrollTimeoutSwitcher : ITickable, IStartable, IDisposable
	{
		private readonly BeatmapPipeline _beatmapPipeline;
		private readonly IMapTime _mapTime;
		private DrumrollClickIntervalParams _clickInterval;

		private readonly SerialDisposable _serialDisposable = new();

		public DrumrollTimeoutSwitcher(BeatmapPipeline beatmapPipeline, IMapTime mapTime)
		{
			_beatmapPipeline = beatmapPipeline;
			_mapTime = mapTime;
		}

		public void Start()
		{
			_serialDisposable.Disposable = _beatmapPipeline.Map
			                                               .Where(map => map != null)
			                                               .Subscribe(OnMapChanged);
		}

		private void OnMapChanged(IBeatmap map)
		{
			_clickInterval = map.GetDifficulty()
			                    .GetDifficultyParams()
			                    .OfType<DrumrollClickIntervalParams>()
			                    .Single();
		}

		public void Tick()
		{
			var element = _beatmapPipeline.Element.CurrentValue;
			if (element is not Drumroll drumroll)
			{
				return;
			}

			var hitTime = drumroll.HitTime;
			var drumrollDuration = drumroll.Duration;
			var mapTime = _mapTime.GetMapTimeInSeconds();
			_clickInterval = _beatmapPipeline.Map
			                                 .CurrentValue
			                                 .GetDifficulty()
			                                 .GetDifficultyParams()
			                                 .OfType<DrumrollClickIntervalParams>()
			                                 .Single();
			if (mapTime > hitTime + drumrollDuration)
			{
				_beatmapPipeline.SwitchToNextElement();
			}
		}

		public void Dispose()
		{
			_serialDisposable.Dispose();
		}
	}
}