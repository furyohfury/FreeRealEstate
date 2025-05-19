using GameEngine;
using UnityEngine;
using Zenject;

namespace Game
{
	public sealed class PlayerController : IInitializable, IFixedTickable
	{
		private readonly PlayerService _playerService;
		private Entity _player;
		private MoveRigidbodyComponent _moveRigidbodyComponent;
		private readonly PlayerInputReader _playerInputReader;
		private RotateComponent _rotateComponent;
		private AttackComponent _attackComponent;

		public PlayerController(PlayerService playerService, PlayerInputReader playerInputReader)
		{
			_playerService = playerService;
			_playerInputReader = playerInputReader;
		}

		public void Initialize()
		{
			_player = _playerService.Player;
			_moveRigidbodyComponent = _player.GetComponent<MoveRigidbodyComponent>();
			_rotateComponent = _player.GetComponent<RotateComponent>();
			_attackComponent = _player.GetComponent<AttackComponent>();
			_playerInputReader.OnLookAction += OnLook;
			_playerInputReader.OnAttackAction += OnAttack;
		}

		public void FixedTick()
		{
			Move();
		}

		private void Move()
		{
			var moveDirection = _playerInputReader.MoveDirection;
			_moveRigidbodyComponent.MoveInDirection(moveDirection * Time.deltaTime);
		}

		private void OnLook(Vector2 direction)
		{
			var horizontalRotation = new Vector3(0, direction.x, 0);
			_rotateComponent.Rotate(horizontalRotation);
		}

		private void OnAttack()
		{
			_attackComponent.Attack();	
		}
	}
}