using System.Collections.Generic;
using Beatmaps;
using ObservableCollections;

namespace Game.Visuals
{
	public sealed class ElementViewsRegistry
	{
		public IReadOnlyObservableDictionary<MapElement, ElementView> ActiveElements => _activeElements;
		private readonly ObservableDictionary<MapElement, ElementView> _activeElements = new();
		public ICollection<ElementView> ElementViews => ((IDictionary<MapElement, ElementView>)_activeElements).Values;

		public void AddPair(ElementViewPair pair)
		{
			_activeElements.Add(pair.MapElement, pair.ElementView);
		}

		public void RemoveElement(MapElement element)
		{
			_activeElements.Remove(element);
		}

		public bool TryGetView(MapElement element, out ElementView view)
		{
			return _activeElements.TryGetValue(element, out view);
		}
	}
}