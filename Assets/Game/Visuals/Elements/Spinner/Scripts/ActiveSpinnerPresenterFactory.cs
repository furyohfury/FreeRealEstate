using Beatmaps;
using Game.BeatmapControl;
using Game.ElementHandle;

namespace Game.Visuals
{
	public sealed class ActiveSpinnerPresenterFactory
	{
		private readonly ElementsClickHandler _elementsClickHandler;
		private readonly BeatmapPipeline _beatmapPipeline;

		public ActiveSpinnerPresenterFactory(ElementsClickHandler elementsClickHandler, BeatmapPipeline beatmapPipeline)
		{
			_elementsClickHandler = elementsClickHandler;
			_beatmapPipeline = beatmapPipeline;
		}

		public ActiveSpinnerPresenter Create(Spinner spinner, ActiveSpinnerView activeSpinnerView)
		{
			var presenter = new ActiveSpinnerPresenter(_elementsClickHandler, _beatmapPipeline);
			presenter.Init(spinner, activeSpinnerView);
			return presenter;
		}
	}
}