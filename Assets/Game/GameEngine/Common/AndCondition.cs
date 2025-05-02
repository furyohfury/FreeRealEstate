using System;
using System.Collections.Generic;

namespace Game
{
	public sealed class AndCondition
	{
		private readonly List<Func<bool>> _conditions = new();

		public void AddCondition(Func<bool> condition)
		{
			_conditions.Add(condition);
		}

		public bool Invoke()
		{
			foreach (var member in _conditions)
			{
				if (!member.Invoke())
				{
					return false;
				}
			}

			return true;
		}
	}
}