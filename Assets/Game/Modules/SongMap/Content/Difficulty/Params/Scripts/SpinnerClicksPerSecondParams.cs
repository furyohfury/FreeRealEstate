using System;
using UnityEngine;

namespace Beatmaps
{
	[Serializable]
	public sealed class SpinnerClicksPerSecondParams : IDifficultyParams
	{
		[SerializeField]
		private float _clicksPerSecond;

		public SpinnerClicksPerSecondParams(float clicksPerSecond)
		{
			_clicksPerSecond = clicksPerSecond;
		}

		public float GetClicksPerSecond()
		{
			return _clicksPerSecond;
		}
	}
}