using System;
using UnityEngine;

namespace Beatmaps
{
	[Serializable]
	public sealed class Drumroll : MapElement
	{
		public float Duration => _duration;
		[SerializeField]
		private float _duration;

		public Drumroll(float hitTime, float duration) : base(hitTime)
		{
			if (duration <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(duration), "Drumroll duration must be positive.");
			}

			_duration = duration;
		}

		public Drumroll()
		{
		}
	}
}