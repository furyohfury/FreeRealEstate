using System;
using Beatmaps;

namespace Game
{
	public class SpinnerTimeoutCalculator : IElementTimeoutCalculator
	{
		public Type GetElementType()
		{
			return typeof(Spinner);
		}
		
		public float GetTimeout(MapElement element, IDifficulty _)
		{
			if (element is not Spinner spinner)
			{
				throw new ArgumentException("Expected drumroll");
			}

			return element.HitTime + spinner.Duration;
		}
	}
}