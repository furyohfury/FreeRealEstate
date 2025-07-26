using System;
using System.Collections.Generic;
using System.Linq;
using Beatmaps;
using Game.SongMapTime;
using UnityEngine;

namespace Game.ElementHandle
{
	public sealed class SpinnerClickStrategy : ElementClickStrategy
	{
		private ClicksPerSecondParams _clicksPerSecondParams;
		private Spinner _activeSpinner;
		private int _doneClicks;

		public SpinnerClickStrategy(IMapTime mapTime) : base(mapTime)
		{
		}

		public override Type GetElementType()
		{
			return typeof(Spinner);
		}

		public override ClickStatus HandleClick(MapElement element, Notes inputNote)
		{
			if (element is not Spinner spinner)
			{
				throw new ArgumentException("Expected spinner");
			}

			if (spinner == _activeSpinner)
			{
				_doneClicks++;
				var clicksNeeded = Mathf.FloorToInt(_clicksPerSecondParams.GetClicksPerSecond() * _activeSpinner.Duration);
				if (_doneClicks >= clicksNeeded)
				{
					return ClickStatus.Success;
				}
			}
			else
			{
				_activeSpinner = spinner;
				_doneClicks = 1;
			}

			return ClickStatus.None;
		}

		public override void SetDifficultyParameters(IEnumerable<IDifficultyParams> parameters)
		{
			_clicksPerSecondParams = parameters?.OfType<ClicksPerSecondParams>().Single();
		}
	}
}