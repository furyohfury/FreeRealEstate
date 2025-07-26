namespace Game.SongMapTime
{
	public sealed class MapTime : IMapTime
	{
		private float _mapTimeSeconds;

		public float GetMapTimeInSeconds()
		{
			return _mapTimeSeconds;
		}

		public void AddTime(float seconds)
		{
			_mapTimeSeconds += seconds;
		}

		public void Reset()
		{
			_mapTimeSeconds = 0f;
		}
	}
}