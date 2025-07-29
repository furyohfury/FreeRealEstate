using System;

namespace Beatmaps
{
	[Serializable]
	public sealed class Drummer : MapElement
	{
		public Drummer(float hitTime) : base(hitTime)
		{
		}
	}
}