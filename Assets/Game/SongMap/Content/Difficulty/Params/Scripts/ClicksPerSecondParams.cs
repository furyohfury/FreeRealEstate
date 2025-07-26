using System;
using UnityEngine;

namespace Beatmaps
{
	[Serializable]
	public sealed class ClicksPerSecondParams : IDifficultyParams
	{
		[SerializeField]
		private float _clicksPerSecond;
		
		public float GetClicksPerSecond()
		{
			return _clicksPerSecond;
		}
	}
}