using System;
using Beatmaps;

namespace Game
{
	public class DrumrollTimeoutCalculator : IElementTimeoutCalculator
	{
		private IDifficulty _difficulty;
		private float _clickInterval;

		public Type GetElementType()
		{
			return typeof(Drumroll);
		}

		public float GetTimeout(MapElement element, IDifficulty mapDifficulty)
		{
			if (element is not Drumroll drumroll)
			{
				throw new ArgumentException("Expected drumroll");
			}

			if (_difficulty != mapDifficulty)
			{
				_difficulty = mapDifficulty;
				_clickInterval = _difficulty.GetDrumrollClickInterval();
			}

			return element.HitTime + drumroll.Duration + _clickInterval;
		}
	}
}