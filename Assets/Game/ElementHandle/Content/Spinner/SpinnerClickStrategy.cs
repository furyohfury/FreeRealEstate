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
		private SpinnerClicksPerSecondParams _spinnerClicksPerSecondParams;
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

		public override ClickResult HandleClick(MapElement element, Notes inputNote)
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
				return new SpinnerRunningClickResult();
			}

			if (IsAlternateNote(inputNote))
			{
				_previousInput = inputNote;
				_doneClicks++;
				var clicksNeeded = Mathf.FloorToInt(_spinnerClicksPerSecondParams.GetClicksPerSecond() * _activeSpinner.Duration);
				Debug.Log($"Processed {_doneClicks} of {clicksNeeded} clicks of spinner");
				if (_doneClicks >= clicksNeeded)
				{
					Debug.Log("Successfully completed spinner");
					return new SpinnerCompleteClickResult();
				}

				return new SpinnerRunningClickResult();
			}

			return new NoneClickResult();
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
			_spinnerClicksPerSecondParams = parameters?.OfType<SpinnerClicksPerSecondParams>().Single();
		}
	}
}