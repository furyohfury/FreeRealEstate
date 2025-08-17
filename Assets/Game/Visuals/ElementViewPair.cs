using Beatmaps;

namespace Game.Visuals
{
	public sealed class ElementViewPair
	{
		public MapElement MapElement => _mapElement;
		public ElementView ElementView => _elementView;

		private readonly MapElement _mapElement;
		private readonly ElementView _elementView;

		public ElementViewPair(MapElement mapElement, ElementView elementView)
		{
			_elementView = elementView;
			_mapElement = mapElement;
		}
	}
}