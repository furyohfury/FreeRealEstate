using System;
using Game.BeatmapControl;
using Game.ElementHandle;
using R3;
using VContainer.Unity;

namespace Game
{
	public sealed class ElementOnStatusSwitcher : IStartable, IDisposable // TODO opt separate
	{
		private readonly BeatmapPipeline _beatmapPipeline;
		private readonly IHandleResultObservable _handleResultObservable;
		private IDisposable _disposable;

		public ElementOnStatusSwitcher(BeatmapPipeline beatmapPipeline, IHandleResultObservable handleResultObservable)
		{
			_beatmapPipeline = beatmapPipeline;
			_handleResultObservable = handleResultObservable;
		}

		public void Start()
		{
			_disposable = _handleResultObservable.OnElementHandled
			                                     .Where(result =>
				                                     result is NoteHitHandleResult
					                                     or SpinnerCompleteHandleResult
					                                     or DrumrollCompleteHandleResult)
			                                     .Subscribe(OnElementHandled);
		}

		private void OnElementHandled(HandleResult result)
		{
			_beatmapPipeline.SwitchToNextElement();
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}