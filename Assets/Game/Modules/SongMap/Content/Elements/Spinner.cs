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
			_duration = duration;
		}
	}
}