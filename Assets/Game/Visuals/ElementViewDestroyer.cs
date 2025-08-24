using Beatmaps;
using UnityEngine;

namespace Game.Visuals
{
	public sealed class ElementViewDestroyer : IElementViewDestroyer
	{
		private readonly ElementViewsRegistry _elementViewsRegistry;

		public ElementViewDestroyer(ElementViewsRegistry elementViewsRegistry)
		{
			_elementViewsRegistry = elementViewsRegistry;
		}

		public void DestroyView(MapElement element)
		{
			if (_elementViewsRegistry.Registry.TryGetValue(element, out var view))
			{
				_elementViewsRegistry.RemoveElement(element);
				Destroy(view);
			}
			else
			{
				Debug.LogWarning($"Couldnt find view for {element} with hittime = {element.HitTime}");
			}
		}

		public void DestroyView(ElementView view)
		{
			_elementViewsRegistry.RemoveElement(view);
			Destroy(view);
		}

		private static void Destroy(ElementView view)
		{
			view.DestroyView();
		}
	}
}