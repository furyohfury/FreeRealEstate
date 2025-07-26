using System;
using UnityEngine;

namespace Beatmaps
{
	[Serializable]
	public abstract class MapElement
	{
		public float TimeSeconds => _timeSeconds;
		[SerializeField]
		private float _timeSeconds;

		protected MapElement(float timeSeconds)
		{
			_timeSeconds = timeSeconds;
		}
	}
}