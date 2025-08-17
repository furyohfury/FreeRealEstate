using Beatmaps;
using R3;

namespace Game
{
	public interface IElementTimeoutObservable
	{
		Observable<MapElement> OnTimeout { get; }
	}
}