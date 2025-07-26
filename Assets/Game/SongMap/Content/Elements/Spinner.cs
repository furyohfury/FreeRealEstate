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

		public Spinner(float timeSeconds, float duration) : base(timeSeconds)
		{
			_duration = duration;
		}
	}
}