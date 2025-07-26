using System;
using Game.ElementHandle;
using R3;
using Beatmaps;
using Game.BeatmapControl;
using VContainer.Unity;

namespace Game
{
	public sealed class ActiveElementOnClickSwitcher : IStartable, IDisposable
	{
		private BeatmapPipeline _beatmapPipeline;
		private readonly ElementsClickHandler _elementsClickHandler;
		private readonly SerialDisposable _disposable = new();

		public ActiveElementOnClickSwitcher(ElementsClickHandler elementsClickHandler, BeatmapPipeline beatmapPipeline)
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