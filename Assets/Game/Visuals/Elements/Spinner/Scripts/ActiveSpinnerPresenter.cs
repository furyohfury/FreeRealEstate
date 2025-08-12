using System;
using System.Linq;
using Beatmaps;
using Game.BeatmapControl;
using Game.ElementHandle;
using R3;
using UnityEngine;

namespace Game.Visuals
{
	public class ActiveSpinnerPresenter : IDisposable
	{
		private readonly ElementsClickHandler _elementsClickHandler;
		private readonly BeatmapPipeline _beatmapPipeline;
		private ActiveSpinnerView _activeSpinnerView;
		private Spinner _spinner;

		private int _clicksNeeded;
		private int _spinnerClicks;
		private readonly CompositeDisposable _disposable = new();

		public ActiveSpinnerPresenter(
			ElementsClickHandler elementsClickHandler,
			BeatmapPipeline beatmapPipeline
		)
		{
			_elementsClickHandler = elementsClickHandler;
			_beatmapPipeline = beatmapPipeline;
		}

		public void Init(Spinner spinner, ActiveSpinnerView activeSpinnerView)
		{
			_spinner = spinner;
			_activeSpinnerView = activeSpinnerView;
			var map = _beatmapPipeline.Map.CurrentValue;
			var clicksPerSecondParams = map
			                            .GetDifficulty()
			                            .GetDifficultyParams()
			                            .OfType<SpinnerClicksPerSecondParams>()
			                            .Single();
			_clicksNeeded = Mathf.FloorToInt(clicksPerSecondParams.GetClicksPerSecond() * _spinner.Duration);
			_spinnerClicks = _clicksNeeded;
			_activeSpinnerView.SetText(_spinnerClicks.ToString());
			_elementsClickHandler.OnClickHandled
			                     .Where(status => status is SpinnerRunningClickResult)
			                     .Subscribe(_ =>
			                     {
				                     _spinnerClicks--;
				                     UpdateView();
			                     })
			                     .AddTo(_disposable);

			_elementsClickHandler.OnClickHandled
			                     .Where(status => status is SpinnerCompleteClickResult or MissClickResult)
			                     .Subscribe(_ =>
			                     {
				                     _activeSpinnerView.Destroy();
				                     _disposable.Clear();
			                     })
			                     .AddTo(_disposable);
		}

		private void UpdateView()
		{
			_activeSpinnerView.SetText(_spinnerClicks.ToString());
			_activeSpinnerView.BounceOuterRing();
			_activeSpinnerView.SetInnerRingScaleRatio((float)_spinnerClicks / _clicksNeeded);
		}

		public void Dispose()
		{
			if (_activeSpinnerView != null)
			{
				_activeSpinnerView.Destroy();
			}

			_disposable.Dispose();
		}
	}
}