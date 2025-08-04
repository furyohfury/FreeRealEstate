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
			_hitTime = hitTime;
		}
	}
}