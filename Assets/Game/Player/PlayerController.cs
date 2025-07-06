using System;
using R3;
using UnityEngine;
using Zenject;

namespace Game
{
	public sealed class PlayerController : IInitializable, IDisposable
	{
		private readonly PlayerService _playerService;
		private readonly PlayerInputReader _playerInputReader;
		private readonly PlayerPointer _playerPointer;

		private readonly CompositeDisposable _disposable = new();

		public PlayerController(PlayerService playerService, PlayerInputReader playerInputReader, PlayerPointer playerPointer)
		{
			_playerService = playerService;
			_playerInputReader = playerInputReader;
			_playerPointer = playerPointer;
		}

		public void Initialize()
		{
			InitMove();
			InitInteract();
			InitGather();
		}

		private void InitMove()
		{
			_playerInputReader.OnMoveAction
			                  .Subscribe(moveDirection =>
			                  {
				                  var player = GetPlayer();
				                  player.Move(moveDirection);
			                  })
			                  .AddTo(_disposable);
		}

		private void InitInteract()
		{
			_playerInputReader.OnInteractAction
			                  .Subscribe(_ =>
			                  {
				                  var player = GetPlayer();
				                  var pointerCollisions = _playerPointer.GetCollisions();
				                  foreach (var pointerCollision in pointerCollisions)
				                  {
					                  if (pointerCollision.TryGetComponent(out IPikminInteractable _))
					                  {
						                  player.SetTargetToPikmins(pointerCollision.gameObject);
						                  return;
					                  }
				                  }
			                  })
			                  .AddTo(_disposable);
		}

		private void InitGather()
		{
			_playerInputReader.OnGatherAction
			                  .Subscribe(_ =>
			                  {
				                  Debug.Log("OnGather");
				                  var player = GetPlayer();
				                  var pointerCollisions = _playerPointer.GetCollisions();
				                  foreach (var pointerCollision in pointerCollisions)
				                  {
					                  if (pointerCollision.TryGetComponent(out IPikminTarget _))
					                  {
						                  player.AddPikmin(pointerCollision.gameObject);
					                  }
				                  }

				                  player.GatherPikmins();
				                  _playerPointer.IncreaseScale();
			                  })
			                  .AddTo(_disposable);
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