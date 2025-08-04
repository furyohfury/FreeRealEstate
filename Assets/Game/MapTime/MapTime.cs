using UnityEngine;

namespace Game.SongMapTime
{
	public sealed class MapTime : IMapTime
	{
		private float _startTimeRealtime;
		private float _pausedAtTime;
		private float _accumulatedPausedTime;
		private bool _isPlaying;

		public void Launch()
		{
			_isPlaying = true;
			_startTimeRealtime = Time.realtimeSinceStartup;
			_accumulatedPausedTime = 0f;
			_pausedAtTime = 0f;
		}

		public void Pause()
		{
			if (!_isPlaying) return;

			_pausedAtTime = GetMapTimeInSeconds();
			_isPlaying = false;
		}

		public void Resume()
		{
			if (_isPlaying) return;

			_accumulatedPausedTime += Time.realtimeSinceStartup - (_startTimeRealtime + _pausedAtTime);
			_isPlaying = true;
		}

		public void Reset()
		{
			_startTimeRealtime = Time.realtimeSinceStartup;
			_accumulatedPausedTime = 0f;
			_pausedAtTime = 0f;
			_isPlaying = false;
		}

		public float GetMapTimeInSeconds()
		{
			if (_isPlaying)
			{
				return Time.realtimeSinceStartup - _startTimeRealtime - _accumulatedPausedTime;
			}

			return _pausedAtTime;
		}
	}
}