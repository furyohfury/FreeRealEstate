using System;
using System.Linq;
using R3;
using UnityEngine;
using Zenject;

namespace Game
{
	public sealed class PlayerController : IInitializable, ITickable, IDisposable
	{
		private readonly PlayerService _playerService;
		private readonly PlayerInputReader _playerInputReader;
		private readonly Transform _playerPointer;

		private readonly CompositeDisposable _disposable = new();

		public PlayerController(PlayerService playerService, PlayerInputReader playerInputReader, Transform playerPointer)
		{
			_playerService = playerService;
			_playerInputReader = playerInputReader;
			_playerPointer = playerPointer;
		}

		public void Initialize()
		{
			InitLook();
			InitInteract();
			InitGather();
		}

		private void InitLook()
		{
			_playerInputReader.OnLookAction
			                  .Subscribe(rot =>
			                  {
				                  var horizontalRotation = new Vector3(0, rot.x, 0);
				                  GetPlayer().Rotate(horizontalRotation);
			                  })
			                  .AddTo(_disposable);
		}

		private void InitInteract()
		{
			_playerInputReader.OnInteractAction
			                  .Subscribe(_ =>
			                  {
				                  Collider[] results = new Collider[5];
				                  var size = Physics.OverlapBoxNonAlloc(_playerPointer.position,
					                  new Vector3(2, 5, 2),
					                  results); // TODO magic numbers

				                  if (size == 0)
				                  {
					                  return;
				                  }

				                  var interactable = results.FirstOrDefault(result => result != null
				                                                                      && result.TryGetComponent(out IPikminInteractable _));
				                  if (interactable != default)
				                  {
					                  GetPlayer().SetTargetToPikmins(interactable.gameObject, false);
				                  }
			                  })
			                  .AddTo(_disposable);
		}

		private void InitGather()
		{
			_playerInputReader.OnGatherAction
			                  .Subscribe(_ =>
			                  {
				                  Collider[] pikmins = Physics.OverlapBox(_playerPointer.position,
					                  new Vector3(2, 5, 2),
					                  Quaternion.identity,
					                  1 << 8); // TODO magic numbers of box and mask

				                  if (pikmins.Length <= 0)
				                  {
					                  return;
				                  }

				                  var player = GetPlayer();
				                  for (int i = 0, count = pikmins.Length; i < count; i++)
				                  {
					                  player.AddPikmin(pikmins[i].gameObject);
				                  }

				                  player.SetTargetToPikmins(player.gameObject, true);
			                  })
			                  .AddTo(_disposable);
		}

		public void Tick()
		{
			Move();
		}

		private void Move()
		{
			var player = GetPlayer();
			var moveDirection = _playerInputReader.MoveDirection;
			moveDirection = player.transform.TransformVector(moveDirection);
			player.Move(moveDirection);
		}

		private void OnLook(Vector2 direction)
		{
			var player = GetPlayer();
			var horizontalRotation = new Vector3(0, direction.x, 0);
			player.Rotate(horizontalRotation);
		}

		private Player GetPlayer()
		{
			var player = _playerService.Player;
			return player;
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}