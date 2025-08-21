using Beatmaps;
using UnityEngine;

namespace Game.Visuals
{
	public sealed class ElementViewDestroyer : IElementViewDestroyer
	{
		private ElementViewsRegistry _elementViewsRegistry;

		public void DestroyView(MapElement element)
		{
			var view = _elementViewsRegistry[element];
			_elementViewsRegistry.RemoveElement(element);
			Destroy(view);
		}

		public void DestroyView(ElementView view)
		{
			_elementViewsRegistry.RemoveElement(view);
			Destroy(view);
		}

		private static void Destroy(ElementView view)
		{
			Object.Destroy(view);
		}
	}
}