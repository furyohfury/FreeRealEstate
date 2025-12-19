using Scellecs.Morpeh;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameEngine
{
	public sealed class InputSystem : ISystem, InputActions.IPlayerActions
	{
		public World World { get; set; }
		private InputActions _inputActions;
		private Vector2 _moveDirection;
		private Filter _filter;
		private Stash<InputComp> _inputStash;

		public void OnAwake()
		{
			_inputActions = new InputActions();
			_inputActions.Enable();
			_inputActions.Player.Enable();
			_inputActions.Player.AddCallbacks(this);

			_inputStash = World.GetStash<InputComp>();
			_filter = World.Filter
			               .With<PlayerTag>()
			               .With<InputComp>()
			               .Build();

			Entity inputEntity = World.CreateEntity();
			_inputStash.Add(inputEntity);
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (Entity entity in _filter)
			{
				ref InputComp inputComp = ref _inputStash.Get(entity);
				inputComp.Direction = _moveDirection;
				Debug.Log("Input: " + inputComp.Direction);
			}
		}

		public void OnMove(InputAction.CallbackContext context)
		{
			if (context.performed)
			{
				_moveDirection = context.ReadValue<Vector2>();
			}
		}

		public void OnLook(InputAction.CallbackContext context)
		{
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
			_inputActions.Player.RemoveCallbacks(this);
			_inputActions.Disable();
		}
	}
}
