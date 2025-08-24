using Beatmaps;

namespace Game.Visuals
{
	public interface IElementViewDestroyer
	{
		void DestroyView(MapElement element);
		void DestroyView(ElementView view);
	}
}