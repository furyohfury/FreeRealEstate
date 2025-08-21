using Beatmaps;

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
			var view = _elementViewsRegistry.Registry[element];
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
			view.DestroyView();
		}
	}
}