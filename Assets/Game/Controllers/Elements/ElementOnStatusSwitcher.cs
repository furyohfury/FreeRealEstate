using System;
using Game.BeatmapControl;
using Game.ElementHandle;
using R3;
using VContainer.Unity;

namespace Game
{
	public sealed class ElementOnStatusSwitcher : IStartable, IDisposable
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
			                                              .Subscribe(OnElementHandled);
		}

		private void OnElementHandled(ClickStatus status)
		{
			if (status is not (ClickStatus.Success or ClickStatus.Fail))
			{
				return;
			}

			_beatmapPipeline.SwitchToNextElement();
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}