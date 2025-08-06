using System;
using UnityEngine;

namespace Beatmaps
{
	[Serializable]
	public abstract class MapElement
	{
		public float HitTime => _hitTime;
		[SerializeField]
		private float _hitTime;

		protected MapElement(float hitTime)
		{
			if (hitTime < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(hitTime), "Spinner duration must be positive.");
			}

			_hitTime = hitTime;
		}

		protected MapElement()
		{
		}
	}
}