using R3;

namespace Game.ElementHandle
{
	public interface IElementHandleEmitter
	{
		Observable<HandleResult> GetStream();
	}
}