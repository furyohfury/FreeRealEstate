using GameEngine;
using UnityEngine;

namespace Game
{
	public sealed class CommonPikmin : Entity
	{
		[SerializeField]
		private LifeComponent _lifeComponent;
		[SerializeField]
		private CarryComponent _carryComponent;
		[SerializeField]
		private MoveRigidbodyComponent _moveRigidbodyComponent;
		[SerializeField]
		private AttackComponent _attackComponent;
		[SerializeField]
		private NavMeshComponent _navMeshComponent;
		[SerializeField]
		private AnimatorComponent _animatorComponent;

		private void Awake()
		{
			AddComponent(_lifeComponent);
			AddComponent(_carryComponent);
			AddComponent(_moveRigidbodyComponent);
			AddComponent(_attackComponent);
		}

		protected override void Update()
		{
			base.Update();

			var isMoving = _navMeshComponent.Velocity != Vector3.zero;
			_animatorComponent.Animator.SetBool(AnimatorHash.IsMoving, isMoving);
		}
	}
}