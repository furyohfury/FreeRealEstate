using System;

namespace Beatmaps
{
	[Serializable]
	public sealed class Drummer : MapElement
	{
		public Drummer(float timeSeconds) : base(timeSeconds)
		{
		}
	}
}