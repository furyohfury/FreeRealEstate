using Game.ElementHandle;

namespace Game.Visuals
{
	public interface IVisualClickHandler : IElementHandler
	{
		void Handle(ElementView view, HandleResult result);
	}
}