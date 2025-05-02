using GameEngine;
using UnityEngine;
using Zenject;

namespace Game
{
	public sealed class PlayerMoveController : IInitializable, IFixedTickable
	{
		private readonly PlayerService _playerService;
		private Entity _player;
		private MoveComponent _moveComponent;
		private readonly PlayerInputReader _playerInputReader;
		private RotateComponent _rotateComponent;

		public PlayerMoveController(PlayerService playerService, PlayerInputReader playerInputReader)
		{
			_playerService = playerService;
			_playerInputReader = playerInputReader;
		}

		public void Initialize()
		{
			_player = _playerService.Player;
			_moveComponent = _player.GetComponent<MoveComponent>();
			_rotateComponent = _player.GetComponent<RotateComponent>();
			_playerInputReader.OnLookAction += OnLook;
		}
		public void FixedTick()
		{
			Move();
		}

		private void Move()
		{
			var moveDirection = _playerInputReader.MoveDirection;
			_moveComponent.Move(moveDirection * Time.deltaTime);
		}

		private void OnLook(Vector2 direction)
		{
			var horizontalRotation = new Vector3(0, direction.x, 0);
			_rotateComponent.Rotate(horizontalRotation);
		}
	}
}