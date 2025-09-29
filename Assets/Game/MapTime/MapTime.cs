namespace Game.BeatmapTime
{
	public sealed class MapTime : IMapTime
	{
		private float _mapTimeSeconds;
		private bool _isPlaying;

		public void Launch()
		{
			_mapTimeSeconds = -3f;
			_isPlaying = true;
		}

		public void Pause()
		{
			_isPlaying = false;
		}

		public void Resume()
		{
			_isPlaying = true;
		}

		public void Reset()
		{
			_mapTimeSeconds = -3f;
			_isPlaying = false;
		}

		public void Tick(float deltaTime)
		{
			if (_isPlaying)
				_mapTimeSeconds += deltaTime;
		}

		public float GetMapTimeInSeconds()
		{
			return _mapTimeSeconds;
		}
	}
}