using System;
using System.Collections.Generic;
using Beatmaps;

namespace Game
{
	public sealed class ElementTimeoutHelper
	{
		private readonly Dictionary<Type, IElementTimeoutCalculator> _timeoutCalculators = new();

		public ElementTimeoutHelper(IEnumerable<IElementTimeoutCalculator> calculators)
		{
			foreach (var calculator in calculators)
			{
				_timeoutCalculators.Add(calculator.GetElementType(), calculator);
			}
		}

		public float GetTimeout(MapElement element, IDifficulty mapDifficulty)
		{
			var elementType = element.GetType();
			var calculator = _timeoutCalculators[elementType];
			var timeout = calculator.GetTimeout(element, mapDifficulty);
			return timeout;
		}
	}
}