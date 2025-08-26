using R3;

namespace Game.BeatmapLaunch
{
	public interface IBeatmapEndObservable
	{
		Observable<Unit> OnCurrentMapEnded { get; }
	}
}