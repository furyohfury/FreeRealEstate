using UnityEngine;

namespace GameEngine
{
	public static class AnimatorHash
	{
		public static readonly int Attack = Animator.StringToHash("Attack");
		public static readonly int IsMoving = Animator.StringToHash("IsMoving");
		public static readonly int Gather = Animator.StringToHash("Gather");
		public static readonly int Interact = Animator.StringToHash("Interact");
		public static readonly int Death = Animator.StringToHash("Death");
	}
}