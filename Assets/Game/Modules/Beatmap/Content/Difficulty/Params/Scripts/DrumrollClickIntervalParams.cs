using System;
using UnityEngine;

namespace Beatmaps
{
	[Serializable]
	public sealed class DrumrollClickIntervalParams : IDifficultyParams
	{
		[SerializeField]
		private float _intervalSeconds;

		public DrumrollClickIntervalParams(float intervalSeconds)
		{
			_intervalSeconds = intervalSeconds;
		}

		public DrumrollClickIntervalParams()
		{
		}

		public float GetClickInterval()
		{
			return _intervalSeconds;
		}
	}
}