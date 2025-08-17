using R3;

namespace Game.ElementHandle
{
	public interface IHandleResultObservable
	{
		Observable<HandleResult> OnElementHandled { get; }
	}
}