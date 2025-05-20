using GameEngine;
using UnityEngine;
using Zenject;

namespace Game
{
	public sealed class PlayerController : IInitializable, ITickable
	{
		private readonly PlayerService _playerService;
		private readonly PlayerInputReader _playerInputReader;
		private Player _player;

		public PlayerController(PlayerService playerService, PlayerInputReader playerInputReader)
		{
			_playerService = playerService;
			_playerInputReader = playerInputReader;
		}

		public void Initialize()
		{
			_player = _playerService.Player;
			_playerInputReader.OnLookAction += OnLook;
		}

		public void Tick()
		{
			Move();
		}

		private void Move()
		{
			var moveDirection = _playerInputReader.MoveDirection;
			_player.Move(moveDirection);
		}

		private void OnLook(Vector2 direction)
		{
			var horizontalRotation = new Vector3(0, direction.x, 0);
			_player.Rotate(horizontalRotation);
		}
	}
}