using System.Collections.Generic;
using Beatmaps;
using ObservableCollections;

namespace Game.Visuals
{
	public sealed class ElementViewsRegistry
	{
		public ICollection<ElementView> RegistryViews => ((IDictionary<MapElement, ElementView>)_registry).Values;
		public IReadOnlyObservableDictionary<MapElement, ElementView> Registry => _registry;

		private readonly ObservableDictionary<MapElement, ElementView> _registry = new();

		public ElementView this[MapElement element] => _registry[element];

		public void AddPair(ElementViewPair pair)
		{
			_registry.Add(pair.MapElement, pair.ElementView);
		}

		public void RemoveElement(MapElement element)
		{
			_registry.Remove(element);
		}

		public void RemoveElement(ElementView view)
		{
			MapElement element = null;
			foreach (var pair in _registry)
			{
				var pairView = pair.Value;
				if (view == pairView)
				{
					element = pair.Key;
					break;
				}
			}

			if (element != null)
			{
				RemoveElement(element);
			}
		}

		public bool TryGetView(MapElement element, out ElementView view)
		{
			return _registry.TryGetValue(element, out view);
		}
	}
}