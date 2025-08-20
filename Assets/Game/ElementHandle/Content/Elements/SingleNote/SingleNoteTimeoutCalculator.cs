using System;
using Beatmaps;

namespace Game
{
	public class SingleNoteTimeoutCalculator : IElementTimeoutCalculator
	{
		private IDifficulty _difficulty;
		private float _clickInterval;

		public Type GetElementType()
		{
			return typeof(SingleNote);
		}

		public float GetTimeout(MapElement element, IDifficulty difficulty)
		{
			if (_difficulty != difficulty)
			{
				_difficulty = difficulty;
				_clickInterval = _difficulty.GetSingleNoteClickInterval();
			}

			var timeout = element.HitTime + _clickInterval;
			return timeout;
		}
	}
}