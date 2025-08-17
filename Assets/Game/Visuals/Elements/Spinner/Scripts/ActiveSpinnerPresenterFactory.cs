using Beatmaps;
using Game.BeatmapControl;
using Game.ElementHandle;

namespace Game.Visuals
{
	public sealed class ActiveSpinnerPresenterFactory
	{
		private readonly BeatmapPipeline _beatmapPipeline;
		private readonly IHandleResultObservable _handleResultObservable;

		public ActiveSpinnerPresenterFactory(BeatmapPipeline beatmapPipeline, IHandleResultObservable handleResultObservable)
		{
			_beatmapPipeline = beatmapPipeline;
			_handleResultObservable = handleResultObservable;
		}

		public ActiveSpinnerPresenter Create(Spinner spinner, ActiveSpinnerView activeSpinnerView)
		{
			var presenter = new ActiveSpinnerPresenter(_beatmapPipeline, _handleResultObservable);
			presenter.Init(spinner, activeSpinnerView);
			return presenter;
		}
	}
}