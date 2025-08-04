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
		private Notes _previousInput;

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

			if (IsActiveSpinner(spinner) == false)
			{
				Debug.Log("Set new spinner");
				_activeSpinner = spinner;
				_previousInput = inputNote;
				_doneClicks = 1;
				return ClickStatus.Running;
			}

			if (IsAlternateNote(inputNote))
			{
				_previousInput = inputNote;
				_doneClicks++;
				var clicksNeeded = Mathf.FloorToInt(_clicksPerSecondParams.GetClicksPerSecond() * _activeSpinner.Duration);
				Debug.Log($"Processed {_doneClicks} of {clicksNeeded} clicks of spinner");
				if (_doneClicks >= clicksNeeded)
				{
					Debug.Log("Successfully completed spinner");
					return ClickStatus.Success;
				}

				return ClickStatus.Running;
			}

			return ClickStatus.None;
		}

		private bool IsAlternateNote(Notes inputNote)
		{
			return _previousInput != inputNote;
		}

		private bool IsActiveSpinner(Spinner spinner)
		{
			return spinner == _activeSpinner;
		}

		public override void SetDifficultyParameters(IEnumerable<IDifficultyParams> parameters)
		{
			_clicksPerSecondParams = parameters?.OfType<ClicksPerSecondParams>().Single();
		}
	}
}