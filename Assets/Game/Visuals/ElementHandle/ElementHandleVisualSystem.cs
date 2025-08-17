using System;
using System.Collections.Generic;
using System.Linq;
using Game.ElementHandle;
using R3;
using UnityEngine;
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
			if (_elementViewsRegistry.TryGetView(element, out ElementView view) == false)
			{
				Debug.LogError($"Couldn't find view for element in registry. {element.GetType().Name} : {element.HitTime}");
				return;
			}

			_visualClickHandlers[element.GetType()].Handle(view, result);
			// kogda ubirat iz registra to?
		}


		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}