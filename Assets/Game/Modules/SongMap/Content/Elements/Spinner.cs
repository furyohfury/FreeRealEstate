using System;
using UnityEngine;

namespace Beatmaps
{
	[Serializable]
	public sealed class Spinner : MapElement
	{
		public float Duration => _duration;
		[SerializeField]
		private float _duration;

		public Spinner(float hitTime, float duration) : base(hitTime)
		{
			if (duration <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(duration), "Spinner duration must be positive.");
			}

			_duration = duration;
		}

		public Spinner()
		{
		}
	}
}