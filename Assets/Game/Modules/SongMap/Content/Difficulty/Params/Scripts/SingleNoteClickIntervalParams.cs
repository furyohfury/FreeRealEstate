using System;
using UnityEngine;

namespace Beatmaps
{
	[Serializable]
	public sealed class SingleNoteClickIntervalParams : IDifficultyParams
	{
		[SerializeField]
		private float _intervalSeconds;

		public SingleNoteClickIntervalParams(float intervalSeconds)
		{
			_intervalSeconds = intervalSeconds;
		}

		public SingleNoteClickIntervalParams()
		{
		}

		public float GetClickInterval()
		{
			return _intervalSeconds;
		}
	}
}