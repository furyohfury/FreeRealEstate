using System;
using Beatmaps;
using Game.Input;
using R3;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace Game
{
	public sealed class InputReader : IInitializable, IDisposable
	{
		public Observable<Notes> OnNotePressed => _onNotePressed;
		private readonly Subject<Notes> _onNotePressed = new();

		private readonly InputActions _inputActions;

		public InputReader(InputActions inputActions)
		{
			_inputActions = inputActions;
		}

		public void Initialize()
		{
			_inputActions.Player.Enable();
			InitNotesStream();
		}

		private void InitNotesStream()
		{
			Observable.Merge(
				          Observable.FromEvent<InputAction.CallbackContext>(
					          h => _inputActions.Player.Red.performed += h,
					          h => _inputActions.Player.Red.performed -= h
				          ).Select(_ => Notes.Red),
				          Observable.FromEvent<InputAction.CallbackContext>(
					          h => _inputActions.Player.Blue.performed += h,
					          h => _inputActions.Player.Blue.performed -= h
				          ).Select(_ => Notes.Blue)
			          )
			          .Subscribe(_onNotePressed.AsObserver());
		}

		public void Dispose()
		{
			_inputActions.Player.Disable();
		}
	}
}