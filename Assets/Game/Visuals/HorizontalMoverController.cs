using System;
using Game.ElementHandle;
using R3;
using VContainer.Unity;

namespace Game.Visuals
{
	public sealed class HorizontalMoverController : IInitializable, IDisposable
	{
		private readonly ElementsHorizontalMover _elementsHorizontalMover;
		private readonly IHandleResultObservable _handleResultObservable;
		private readonly ElementsVisualsSpawner _visualsSpawner;

		private readonly CompositeDisposable _disposable = new();

		public HorizontalMoverController(
			ElementsHorizontalMover elementsHorizontalMover,
			IHandleResultObservable handleResultObservable,
			ElementsVisualsSpawner visualsSpawner
		)
		{
			_elementsHorizontalMover = elementsHorizontalMover;
			_handleResultObservable = handleResultObservable;
			_visualsSpawner = visualsSpawner;
		}


		public void Initialize()
		{
			_visualsSpawner.OnElementViewSpawned
			               .Subscribe(pair => _elementsHorizontalMover.Add(pair.MapElement, pair.ElementView))
			               .AddTo(_disposable);

			_handleResultObservable.OnElementHandled
			                       .Subscribe(result => { _elementsHorizontalMover.Remove(result.Element); })
			                       .AddTo(_disposable);
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}