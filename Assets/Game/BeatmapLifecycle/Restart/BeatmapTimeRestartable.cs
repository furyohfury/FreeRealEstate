using Game.BeatmapTime;

namespace Game.BeatmapRestart
{
	public sealed class BeatmapTimeRestartable : IBeatmapRestartable
	{
		private readonly IMapTime _mapTime;

		public BeatmapTimeRestartable(IMapTime mapTime)
		{
			_mapTime = mapTime;
		}

		public void Restart()
		{
			_mapTime.Reset();
		}
	}
}