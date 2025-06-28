using System;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using Zenject;

namespace Game
{
	public sealed class PlayerInputReader : IInitializable, IDisposable
	{
		public readonly Observable<Vector3> OnMoveAction;
		public readonly Observable<Vector2> OnLookAction;
		public readonly Observable<Unit> OnInteractAction;
		public readonly Observable<Unit> OnGatherAction;

		private readonly InputControls _inputControls;

		private readonly CompositeDisposable _disposable = new();

		public PlayerInputReader(InputControls inputControls)
		{
			_inputControls = inputControls;

			OnLookAction = Observable.FromEvent<InputAction.CallbackContext>(
				                         h => _inputControls.Player.Look.performed += h,
				                         h => _inputControls.Player.Look.performed -= h)
			                         .Select(ctx => ctx.ReadValue<Vector2>());

			OnMoveAction = Observable.EveryUpdate()
			                         .Select(_ =>
			                         {
				                         var direction = _inputControls.Player.Move.ReadValue<Vector2>();
				                         return new Vector3(direction.x, 0, direction.y);
			                         });

			OnInteractAction = Observable.FromEvent<InputAction.CallbackContext>(
				h => _inputControls.Player.Interact.performed += h,
				h => _inputControls.Player.Interact.performed -= h)
			                             .Select(_ => Unit.Default);

			OnGatherAction = Observable.EveryUpdate()
			                           .Where(_ => _inputControls.Player.Gather.IsPressed())
			                           .AsUnitObservable();
		}

		public void Initialize()
		{
			_inputControls.Player.Enable();
		}

		public void Dispose()
		{
			_disposable.Dispose();
			_inputControls.Player.Disable();
		}
	}
}