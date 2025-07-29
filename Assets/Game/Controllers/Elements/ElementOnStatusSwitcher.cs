using System;
using Game.BeatmapControl;
using Game.ElementHandle;
using R3;
using VContainer.Unity;

namespace Game
{
	public sealed class ActiveElementOnStatusSwitcher : IStartable, IDisposable
	{
		private readonly BeatmapPipeline _beatmapPipeline;
		private readonly ElementsClickHandler _elementsClickHandler;
		private readonly SerialDisposable _disposable = new();

		public ActiveElementOnStatusSwitcher(ElementsClickHandler elementsClickHandler, BeatmapPipeline beatmapPipeline)
		{
			_elementsClickHandler = elementsClickHandler;
			_beatmapPipeline = beatmapPipeline;
		}

		public void Start()
		{
			_disposable.Disposable = Observable.FromEvent<ClickStatus>(
				                                   h => _elementsClickHandler.OnElementHandled += h,
				                                   h => _elementsClickHandler.OnElementHandled -= h)
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