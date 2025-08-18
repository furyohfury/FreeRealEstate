using Game.ElementHandle;

namespace Game.Visuals
{
	public interface IVisualClickHandler : IElementHandler
	{
		void Handle(HandleResult result);
	}
}