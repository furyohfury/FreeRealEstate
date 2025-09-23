using System;
using Game.Input;
using R3;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace Game
{
	public sealed class InputRestartable : IInputRestartable, IInitializable, IDisposable
	{
		public Observable<Unit> OnRestart { get; private set; }

		private readonly InputActions _inputActions;

		public InputRestartable(InputActions inputActions)
		{
			_inputActions = inputActions;
		}

		public void Initialize()
		{
			_inputActions.UI.Restart.Enable();
			OnRestart = Observable.FromEvent<InputAction.CallbackContext>(
				                      h => _inputActions.UI.Restart.performed += h,
				                      h => _inputActions.UI.Restart.performed -= h
				                      )
			                      .Select(_ => Unit.Default);
		}

		public void Dispose()
		{
			_inputActions.UI.Restart.Disable();
		}
	}
}