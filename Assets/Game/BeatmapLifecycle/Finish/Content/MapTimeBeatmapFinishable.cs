using Game.BeatmapTime;

namespace Game.BeatmapFinish
{
	public sealed class MapTimeBeatmapFinishable : IBeatmapFinishable
	{
		private readonly IMapTime _mapTime;

		public MapTimeBeatmapFinishable(IMapTime mapTime)
		{
			_mapTime = mapTime;
		}

		public void Finish()
		{
			_mapTime.Pause();
		}
	}
}