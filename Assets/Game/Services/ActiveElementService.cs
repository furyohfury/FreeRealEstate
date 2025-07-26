using R3;
using Beatmaps;

namespace Game
{
	public sealed class ActiveElementService
	{
		public ReadOnlyReactiveProperty<MapElement> Element => _element;
		private readonly ReactiveProperty<MapElement> _element = new();

		public void SetElement(MapElement element)
		{
			_element.Value = element;
		}
	}
}