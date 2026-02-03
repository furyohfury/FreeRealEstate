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
		private Stash<Input> _inputStash;

		public void OnAwake()
		{
			_inputActions = new InputActions();
			_inputActions.Enable();
			_inputActions.Player.Enable();
			_inputActions.Player.AddCallbacks(this);

			_inputStash = World.GetStash<Input>();
			_filter = World.Filter
			               .With<PlayerTag>()
			               .With<Input>()
			               .Build();

			Entity inputEntity = World.CreateEntity();
			_inputStash.Add(inputEntity);
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (Entity entity in _filter)
			{
				ref Input input = ref _inputStash.Get(entity);
				input.Direction = _inputActions.Player.Move.ReadValue<Vector2>();
			}
		}

		public void OnMove(InputAction.CallbackContext context)
		{
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
