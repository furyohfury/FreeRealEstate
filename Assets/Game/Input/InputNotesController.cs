using System;
using Beatmaps;
using Game.BeatmapControl;
using Game.ElementHandle;
using Game.SongMapTime;
using R3;
using VContainer.Unity;

namespace Game
{
	public sealed class InputNotesController : IStartable, IDisposable
	{
		private readonly InputReader _inputReader;
		private readonly ElementsClickHandler _elementsClickHandler;
		private readonly IMapTime _mapTime;
		private readonly BeatmapPipeline _beatmapPipeline;

		private readonly CompositeDisposable _disposable = new();
		private const float INACTIVE_CLICK_THRESHOLD = 1f;

		public InputNotesController(
			InputReader inputReader,
			ElementsClickHandler elementsClickHandler,
			IMapTime mapTime, BeatmapPipeline beatmapPipeline)
		{
			_inputReader = inputReader;
			_elementsClickHandler = elementsClickHandler;
			_mapTime = mapTime;
			_beatmapPipeline = beatmapPipeline;
		}

		public void Start()
		{
			Observable.FromEvent<Notes>(
				          h => _inputReader.OnNotePressed += h,
				          h => _inputReader.OnNotePressed -= h)
			          .Subscribe(OnNotePressed)
			          .AddTo(_disposable);
		}

		private void OnNotePressed(Notes note)
		{
			var activeMapElement = _beatmapPipeline.Element.CurrentValue;
			if (IsTooFar(activeMapElement))
			{
				return;
			}

			_elementsClickHandler.HandleElement(activeMapElement, note);
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