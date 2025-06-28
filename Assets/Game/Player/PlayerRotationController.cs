using GameEngine;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game
{
	public sealed class PlayerRotationController : ITickable, IInitializable
	{
		private IRotateable _rotateable;
		private readonly PlayerService _playerService;
		private readonly Camera _camera;
		private float _offset;
		private readonly int _groundLayer = 1 << 10;

		public PlayerRotationController(PlayerService playerService, Camera camera)
		{
			_playerService = playerService;
			_camera = camera;
		}

		public void Initialize()
		{
			_rotateable = _playerService.Player.GetComponent<IRotateable>();
		}

		public void Tick()
		{
			var ray = _camera.ScreenPointToRay(Mouse.current.position.value);
			var raycast = Physics.Raycast(ray, out var hit, 10000, _groundLayer);
			if (!raycast)
			{
				Debug.LogError("Cant raycast ground");
				return;
			}

			var playerTransform = _playerService.Player.transform;
			var hitPos = hit.point;
			Vector3 lookDirection = hitPos - playerTransform.position;
			lookDirection.y = 0f; // Игнорируем вертикальное вращение

			if (lookDirection.sqrMagnitude > 0.001f)
			{
				Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
				_rotateable.RotateTo(targetRotation);
			}
		}
	}
}