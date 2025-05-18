using UnityEngine;

namespace GameEngine
{
	public static class AnimatorHash
	{
		public static readonly int Attack = Animator.StringToHash("Attack");
		public static readonly int IsMoving = Animator.StringToHash("IsMoving");
	}
}