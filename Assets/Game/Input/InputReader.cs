using System;
using Beatmaps;
using Game.Input;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace Game
{
	public sealed class InputReader : InputActions.IPlayerActions, IStartable, IDisposable
	{
		public event Action<Notes> OnNotePressed;

		private readonly InputActions _inputActions;

		public InputReader(InputActions inputActions)
		{
			_inputActions = inputActions;
		}

		public void Start()
		{
			_inputActions.Player.SetCallbacks(this);
			_inputActions.Player.Enable();
		}

		void InputActions.IPlayerActions.OnRed(InputAction.CallbackContext context)
		{
			if (context.phase == InputActionPhase.Performed)
			{
				OnNotePressed?.Invoke(Notes.Red);
			}
		}

		void InputActions.IPlayerActions.OnBlue(InputAction.CallbackContext context)
		{
			if (context.phase == InputActionPhase.Performed)
			{
				OnNotePressed?.Invoke(Notes.Blue);
			}
		}

		public void OnTestNote(Notes note)
		{
			OnNotePressed?.Invoke(note);
		}

		public void Dispose()
		{
			_inputActions.Player.Disable();
		}
	}
}