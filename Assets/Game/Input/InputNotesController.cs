using System;
using Beatmaps;
using Game.ElementHandle;
using Game.SongMapTime;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace Game
{
	public sealed class InputNotesController : IStartable, IDisposable
	{
		private readonly InputReader _inputReader;
		private readonly ElementsClickHandler _elementsClickHandler;
		private readonly ActiveElementService _activeElementService;
		private readonly IMapTime _mapTime;

		private readonly CompositeDisposable _disposable = new();
		private const float INACTIVE_CLICK_THRESHOLD = 1f;

		public InputNotesController(
			InputReader inputReader,
			ElementsClickHandler elementsClickHandler,
			ActiveElementService activeElementService,
			IMapTime mapTime
		)
		{
			_inputReader = inputReader;
			_elementsClickHandler = elementsClickHandler;
			_activeElementService = activeElementService;
			_mapTime = mapTime;
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
			var activeMapElement = _activeElementService.Element.CurrentValue;
			if (IsOutsideTimeThreshold(activeMapElement))
			{
				return;
			}

			_elementsClickHandler.HandleElement(activeMapElement, note);
		}

		private bool IsOutsideTimeThreshold(MapElement activeMapElement)
		{
			return Mathf.Abs(activeMapElement.TimeSeconds - _mapTime.GetMapTimeInSeconds()) >= INACTIVE_CLICK_THRESHOLD;
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}