using System;
using Game;
using UnityEngine;

namespace GameEngine
{
	[Serializable]
	public sealed class AnimatorComponent
	{
		public Animator Animator => _animator;
		public AnimatorEventReceiver EventReceiver => _animatorEventReceiver;

		[SerializeField]
		private Animator _animator;
		[SerializeField]
		private AnimatorEventReceiver _animatorEventReceiver;
	}
}