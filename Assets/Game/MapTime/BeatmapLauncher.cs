namespace Game.SongMapTime
{
	public sealed class BeatmapLauncher
	{
		private readonly MapTime _mapTime;

		public BeatmapLauncher(MapTime mapTime)
		{
			_mapTime = mapTime;
		}

		public void LaunchActiveMap()
		{
			_mapTime.Launch();
		}
	}
}