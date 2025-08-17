using Beatmaps;

namespace Game.ElementHandle
{
	public abstract class HandleResult
	{
		public MapElement Element => _mapElement;
		private readonly MapElement _mapElement;

		protected HandleResult(MapElement mapElement)
		{
			_mapElement = mapElement;
		}
	}
}