using Game.BeatmapControl;
using R3;
using VContainer.Unity;

namespace Game.BeatmapLaunch
{
	public sealed class BeatmapEndObservable : IInitializable, IBeatmapEndObservable
	{
		public Observable<Unit> OnCurrentMapEnded => _onCurrentMapEnded;
		private Observable<Unit> _onCurrentMapEnded;
		private readonly BeatmapPipeline _beatmapPipeline;

		public BeatmapEndObservable(BeatmapPipeline beatmapPipeline)
		{
			_beatmapPipeline = beatmapPipeline;
		}

		public void Initialize()
		{
			_onCurrentMapEnded = Observable.EveryUpdate()
			                               .Where(_ => _beatmapPipeline.IsEnded);
		}
	}
}