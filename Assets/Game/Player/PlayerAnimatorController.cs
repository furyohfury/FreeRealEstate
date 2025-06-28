using System;
using GameEngine;
using R3;
using UnityEngine;
using Zenject;

namespace Game
{
	public sealed class PlayerAnimatorController : IInitializable, IDisposable
	{
		private readonly PlayerService _playerService;
		private Animator _playerAnimator;
		private readonly PlayerInputReader _playerInputReader;
		private readonly CompositeDisposable _disposable = new();

		public PlayerAnimatorController(PlayerService playerService, PlayerInputReader playerInputReader)
		{
			_playerService = playerService;
			_playerInputReader = playerInputReader;
		}

		public void Initialize()
		{
			_playerAnimator = _playerService.Player.Animator;

			_playerInputReader.OnGatherAction
			                  .Subscribe(_ => _playerAnimator.SetTrigger(AnimatorHash.Gather))
			                  .AddTo(_disposable);

			_playerInputReader.OnInteractAction
			                  .Subscribe(_ => _playerAnimator.SetTrigger(AnimatorHash.Interact))
			                  .AddTo(_disposable);
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}