using System;
using System.Linq;
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

		private const string GATHER_ANIMATION_NAME = "CharacterArmature_Wave";

		public PlayerAnimatorController(PlayerService playerService, PlayerInputReader playerInputReader)
		{
			_playerService = playerService;
			_playerInputReader = playerInputReader;
		}

		public void Initialize()
		{
			_playerAnimator = _playerService.Player.Animator;

			float gatherClipLength = 0;
			try
			{
				AnimationClip gatherClip = _playerAnimator
				                           .runtimeAnimatorController
				                           .animationClips
				                           .Single(clip => clip.name == GATHER_ANIMATION_NAME);
				gatherClipLength = gatherClip.length;
			}
			catch (NullReferenceException _)
			{
				throw new NullReferenceException("Invalid name for gather clip");
			}

			_playerInputReader.OnGatherAction
			                  .ThrottleFirst(TimeSpan.FromSeconds(gatherClipLength))
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