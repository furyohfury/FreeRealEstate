using R3;

namespace Game.ElementHandle
{
	public interface IHandleResultStrategy
	{
		Observable<HandleResult> GetStream();
	}
}