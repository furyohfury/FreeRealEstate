using UnityEngine;

namespace Beatmaps
{
	public sealed class ClickIntervalParams : IDifficultyParams
	{
		[SerializeField]
		private float _intervalSeconds;

		public float GetClickInterval()
		{
			return _intervalSeconds;
		}
	}
}