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
			_duration = duration;
		}
	}
}