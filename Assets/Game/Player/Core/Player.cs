using GameEngine;
using R3;
using UnityEngine;

namespace Game
{
	public sealed class Player : MonoBehaviour,
		IHitPoints,
		ITakeDamage,
		IMoveable,
		IRotateable,
		IAnimator,
		IPikminInteractable
	{
		public int HitPoints => _lifeComponent.CurrentHealth.CurrentValue;

		public Vector3 Position => transform.position;
		public Animator Animator => _animatorComponent.Animator;

		[SerializeField]
		private MoveCharControllerComponent _moveCharControllerComponent;
		[SerializeField]
		private LifeComponent _lifeComponent;
		[SerializeField]
		private RotateTransformComponent _rotateTransformComponent;
		[SerializeField]
		private AnimatorComponent _animatorComponent;
		[SerializeField]
		private PikminControlComponent _pikminControlComponent;

		private readonly CompositeDisposable _disposable = new();

		private void Awake()
		{
			_moveCharControllerComponent.CanMove.AddCondition(() => _lifeComponent.IsAlive);
			_rotateTransformComponent.CanRotate.AddCondition(() => _lifeComponent.IsAlive);
		}

		private void Update()
		{
			AnimateMovement();
		}

		private void AnimateMovement()
		{
			var isMoving = _moveCharControllerComponent.IsMoving;
			_animatorComponent.Animator.SetBool(AnimatorHash.IsMoving, isMoving);
		}

		public void TakeDamage(int delta)
		{
			_lifeComponent.ChangeHealth(delta);
		}

		public void Move(Vector3 direction)
		{
			_moveCharControllerComponent.Move(direction);
		}

		public void MoveTo(Vector3 position)
		{
			transform.position = position;
		}

		public void Rotate(Vector3 delta)
		{
			_rotateTransformComponent.Rotate(delta);
		}

		public void RotateTo(Quaternion direction)
		{
			_rotateTransformComponent.RotateTo(direction);
		}

		public void AddPikmin(GameObject pikmin)
		{
			_pikminControlComponent.AddPikmin(pikmin);
		}

		public void SetTargetToPikmins(GameObject target, bool isPlayer)
		{
			_pikminControlComponent.SetTargetToPikmins(target, isPlayer);
		}

		private void OnDestroy()
		{
			_disposable.Dispose();
		}
	}
}