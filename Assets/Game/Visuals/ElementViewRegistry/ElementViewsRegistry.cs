using System.Collections.Generic;
using Beatmaps;
using ObservableCollections;

namespace Game.Visuals
{
	public sealed class ElementViewsRegistry
	{
		public ICollection<ElementView> ActiveElementViews => ((IDictionary<MapElement, ElementView>)_activeElements).Values;
		public ICollection<ElementView> InactiveElementViews => ((IDictionary<MapElement, ElementView>)_inactiveElements).Values;
		public IReadOnlyObservableDictionary<MapElement, ElementView> ActiveElements => _activeElements;
		public IReadOnlyObservableDictionary<MapElement, ElementView> InactiveElements => _inactiveElements;

		private readonly ObservableDictionary<MapElement, ElementView> _activeElements = new();
		private readonly ObservableDictionary<MapElement, ElementView> _inactiveElements = new();

		public void AddPair(ElementViewPair pair)
		{
			_activeElements.Add(pair.MapElement, pair.ElementView);
		}

		public void RemoveElement(MapElement element)
		{
			_activeElements.Remove(element);
			_inactiveElements.Remove(element);
		}

		public bool TryGetView(MapElement element, out ElementView view)
		{
			return _activeElements.TryGetValue(element, out view);
		}

		public void SetInactive(MapElement element)
		{
			if (_activeElements.Remove(element, out var view))
			{
				_inactiveElements.Add(element, view);
			}
		}
	}
}