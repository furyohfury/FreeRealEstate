using System;
using Beatmaps;
using Game.BeatmapControl;
using Game.BeatmapTime;
using Game.ElementHandle;
using R3;
using VContainer.Unity;

namespace Game
{
	public sealed class InputNotesController : IInitializable, IDisposable
	{
		private readonly InputReader _inputReader;
		private readonly ClickHandleResultStrategy _clickHandleResultStrategy;
		private readonly IMapTime _mapTime;
		private readonly BeatmapPipeline _beatmapPipeline;

		private IDisposable _disposable;
		private const float INACTIVE_CLICK_THRESHOLD = 0.5f;

		public InputNotesController(
			InputReader inputReader,
			ClickHandleResultStrategy clickHandleResultStrategy,
			IMapTime mapTime,
			BeatmapPipeline beatmapPipeline
		)
		{
			_inputReader = inputReader;
			_clickHandleResultStrategy = clickHandleResultStrategy;
			_mapTime = mapTime;
			_beatmapPipeline = beatmapPipeline;
		}

		public void Initialize()
		{
			_disposable = _inputReader.OnNotePressed
			                          .WithLatestFrom(_beatmapPipeline.Element, (note, element) => (note, element))
			                          .Subscribe(tuple =>
			                          {
				                          var (note, element) = tuple;

				                          if (IsTooFar(element))
				                          {
					                          return;
				                          }

				                          _clickHandleResultStrategy.HandleElement(element, note);
			                          });
		}

		private bool IsTooFar(MapElement activeMapElement)
		{
			return activeMapElement.HitTime - _mapTime.GetMapTimeInSeconds() >= INACTIVE_CLICK_THRESHOLD;
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}