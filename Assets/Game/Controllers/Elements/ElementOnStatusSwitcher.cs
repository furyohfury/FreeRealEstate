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
		private readonly ElementsClickHandler _elementsClickHandler;
		private readonly SerialDisposable _disposable = new();

		public ElementOnStatusSwitcher(ElementsClickHandler elementsClickHandler, BeatmapPipeline beatmapPipeline)
		{
			_elementsClickHandler = elementsClickHandler;
			_beatmapPipeline = beatmapPipeline;
		}

		public void Start()
		{
			_disposable.Disposable = _elementsClickHandler.OnClickHandled
			                                              .Where(result =>
				                                              result is HitClickResult
					                                              or SpinnerCompleteClickResult
					                                              or DrumrollCompleteClickResult)
			                                              .Subscribe(OnElementHandled);
		}

		private void OnElementHandled(ClickResult result)
		{
			_beatmapPipeline.SwitchToNextElement();
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}