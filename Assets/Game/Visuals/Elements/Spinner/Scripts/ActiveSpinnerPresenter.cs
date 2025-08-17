using System;
using Beatmaps;
using Game.BeatmapControl;
using Game.ElementHandle;
using R3;
using UnityEngine;

namespace Game.Visuals
{
	public class ActiveSpinnerPresenter : IDisposable
	{
		private readonly IHandleResultObservable _handleResultObservable;
		private readonly BeatmapPipeline _beatmapPipeline;
		private ActiveSpinnerView _activeSpinnerView;
		private Spinner _spinner;

		private int _clicksNeeded;
		private int _spinnerClicks;
		private readonly CompositeDisposable _disposable = new();

		public ActiveSpinnerPresenter(BeatmapPipeline beatmapPipeline, IHandleResultObservable handleResultObservable)
		{
			_beatmapPipeline = beatmapPipeline;
			_handleResultObservable = handleResultObservable;
		}

		public void Init(Spinner spinner, ActiveSpinnerView activeSpinnerView)
		{
			_spinner = spinner;
			_activeSpinnerView = activeSpinnerView;
			var map = _beatmapPipeline.Map.CurrentValue;
			var clicksPerSecond = map
			                      .GetDifficulty()
			                      .GetSpinnerClicksPerSecond();
			_clicksNeeded = Mathf.FloorToInt(clicksPerSecond * _spinner.Duration);
			_spinnerClicks = _clicksNeeded;
			_activeSpinnerView.SetText(_spinnerClicks.ToString());
			_handleResultObservable.OnElementHandled
			                       .Where(status => status is SpinnerRunningHandleResult)
			                       .Subscribe(_ =>
			                       {
				                       _spinnerClicks--;
				                       UpdateView();
			                       })
			                       .AddTo(_disposable);

			_handleResultObservable.OnElementHandled
			                       .Where(status => status is SpinnerCompleteHandleResult or MissHandleResult)
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