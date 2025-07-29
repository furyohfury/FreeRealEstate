using Beatmaps;
using Game.BeatmapControl;
using Game.SongMapTime;
using VContainer.Unity;

namespace Game
{
	public sealed class SpinnerTimeoutSwitcher : ITickable
	{
		private readonly BeatmapPipeline _beatmapPipeline;
		private readonly IMapTime _mapTime;

		public SpinnerTimeoutSwitcher(BeatmapPipeline beatmapPipeline, IMapTime mapTime)
		{
			_beatmapPipeline = beatmapPipeline;
			_mapTime = mapTime;
		}

		public void Tick()
		{
			var element = _beatmapPipeline.Element.CurrentValue;
			if (element is not Spinner spinner)
			{
				return;
			}

			var hitTime = spinner.HitTime;
			var spinnerDuration = spinner.Duration;
			var mapTime = _mapTime.GetMapTimeInSeconds();
			if (mapTime > hitTime + spinnerDuration)
			{
				_beatmapPipeline.SwitchToNextElement();
			}
		}
	}
}