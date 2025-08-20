using Beatmaps;
using Game.ElementHandle;

namespace Game
{
	public interface IElementTimeoutCalculator : IElementHandler
	{
		float GetTimeout(MapElement element, IDifficulty difficulty);
	}
}