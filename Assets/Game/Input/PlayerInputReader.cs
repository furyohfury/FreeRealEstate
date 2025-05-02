using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game
{
	public sealed class PlayerInputReader : IInitializable, IDisposable, InputControls.IPlayerActions
	{
		public Vector3 MoveDirection => _moveDirection;
		public event Action<Vector2> OnLookAction; 

		private readonly InputControls _inputControls;
		private Vector3 _moveDirection;

		public PlayerInputReader(InputControls inputControls)
		{
			_inputControls = inputControls;
		}

		public void Initialize()
		{
			_inputControls.Player.SetCallbacks(this);
			_inputControls.Player.Enable();
		}

		public void OnMove(InputAction.CallbackContext context)
		{
			var direction = context.ReadValue<Vector2>();
			_moveDirection = new Vector3(direction.x, 0, direction.y);
		}

		public void OnLook(InputAction.CallbackContext context)
		{
			var direction = context.ReadValue<Vector2>();
			OnLookAction?.Invoke(direction);
		}

		public void OnAttack(InputAction.CallbackContext context)
		{
			
		}

		public void OnInteract(InputAction.CallbackContext context)
		{
			
		}

		public void OnCrouch(InputAction.CallbackContext context)
		{
			
		}

		public void OnJump(InputAction.CallbackContext context)
		{
			
		}

		public void OnPrevious(InputAction.CallbackContext context)
		{
			
		}

		public void OnNext(InputAction.CallbackContext context)
		{
			
		}

		public void OnSprint(InputAction.CallbackContext context)
		{
			
		}

		public void Dispose()
		{
			_inputControls.Player.Disable();
		}
	}
}