using System;
using System.Collections.Generic;
using System.Linq;
using Game.ElementHandle;
using R3;
using VContainer.Unity;

namespace Game.Visuals
{
	public sealed class ElementHandleVisualSystem : IInitializable, IDisposable
	{
		private readonly Dictionary<Type, IVisualClickHandler> _visualClickHandlers;
		private readonly ElementViewsRegistry _elementViewsRegistry;
		private readonly IHandleResultObservable _handleResultObservable;

		private IDisposable _disposable;

		public ElementHandleVisualSystem(
			IEnumerable<IVisualClickHandler> visualClickHandlers,
			ElementViewsRegistry elementViewsRegistry,
			IHandleResultObservable handleResultObservable
		)
		{
			_visualClickHandlers = visualClickHandlers.ToDictionary(handler => handler.GetElementType(), handler => handler);
			_elementViewsRegistry = elementViewsRegistry;
			_handleResultObservable = handleResultObservable;
		}


		public void Initialize()
		{
			_disposable = _handleResultObservable.OnElementHandled
			                                     .Subscribe(OnHandleResultReceived);
		}

		private void OnHandleResultReceived(HandleResult result)
		{
			var element = result.Element;
			_visualClickHandlers[element.GetType()].Handle(result);
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}