using System;
using Game.ElementHandle;
using R3;
using Beatmaps;
using VContainer.Unity;

namespace Game
{
	public sealed class ActiveElementOnClickSwitcher : IStartable, IDisposable
	{
		private ActiveElementSwitcher _switcher;
		private readonly ElementsClickHandler _elementsClickHandler;
		private readonly SerialDisposable _disposable = new();

		public ActiveElementOnClickSwitcher(ActiveElementSwitcher switcher, ElementsClickHandler elementsClickHandler)
		{
			_switcher = switcher;
			_elementsClickHandler = elementsClickHandler;
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

			_switcher.SetNextElement();
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}