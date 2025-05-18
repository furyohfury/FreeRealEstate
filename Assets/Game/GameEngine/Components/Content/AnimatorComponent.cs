using System;
using UnityEngine;

namespace GameEngine
{
	[Serializable]
	public sealed class AnimatorComponent : IComponent
	{
		public Animator Animator => _animator;
		
		[SerializeField]
		private Animator _animator;
	}
}