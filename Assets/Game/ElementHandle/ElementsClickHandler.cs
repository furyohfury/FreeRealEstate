using System;
using System.Collections.Generic;
using Beatmaps;
using R3;

namespace Game.ElementHandle
{
	public sealed class ElementsClickHandler
	{
		public Observable<ClickStatus> OnClickHandled => _onClickHandled;
		private Subject<ClickStatus> _onClickHandled = new Subject<ClickStatus>();
		private readonly Dictionary<Type, ElementClickStrategy> _handlers = new();

		public ElementsClickHandler(IEnumerable<ElementClickStrategy> handlers)
		{
			foreach (var clickHandler in handlers)
			{
				_handlers.TryAdd(clickHandler.GetElementType(), clickHandler);
			}
		}	

		public void SetDifficulty(IDifficulty difficulty)
		{
			foreach (var handler in _handlers.Values)
			{
				handler.SetDifficultyParameters(difficulty.GetDifficultyParams());
			}
		}

		public void HandleElement(MapElement element, Notes note)
		{
			var type = element.GetType();
			var handler = _handlers[type];
			var clickStatus = handler.HandleClick(element, note);
			_onClickHandled.OnNext(clickStatus);
		}
	}
}