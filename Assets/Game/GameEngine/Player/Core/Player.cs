using System;
using GameEngine;
using R3;
using UnityEngine;

namespace Game
{
	[SelectionBase]
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

		[Header("FX")]
		[SerializeField]
		private PikminGatherSFXComponent _gatherSfxComponent;
		[SerializeField]
		private PikminInteractSFXComponent _interactSfxComponent;
		[SerializeField]
		private MoveSFXComponent _moveSfxComponent;
		private float _whistleDelay = 0f;

		private readonly CompositeDisposable _disposable = new();

		private void Awake()
		{
			_moveCharControllerComponent.CanMove.AddCondition(() => _lifeComponent.IsAlive);
			_rotateTransformComponent.CanRotate.AddCondition(() => _lifeComponent.IsAlive);
		}

		private void Start()
		{
			Observable.EveryUpdate()
			          .Subscribe(_ => AnimateMovement())
			          .AddTo(_disposable);

			Observable.EveryUpdate()
			          .Subscribe(_ =>
			          {
				          _whistleDelay = Mathf.Max(0, _whistleDelay - Time.deltaTime);
				          if (_whistleDelay <= 0)
				          {
					          _gatherSfxComponent.Stop();
				          }
			          })
			          .AddTo(_disposable);
			
			Observable.EveryUpdate()
			          .Subscribe(_ =>
			          {
				          var isMoving = _moveCharControllerComponent.IsMoving;
				          if (isMoving)
				          {
					          _moveSfxComponent.Play();
				          }
				          else
				          {
					          _moveSfxComponent.Stop();
				          }
			          })
			          .AddTo(_disposable);
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

		public void SetTargetToPikmins(GameObject target)
		{
			_pikminControlComponent.SetTargetToPikmins(target, false);
			_interactSfxComponent.PlaySFX();
		}

		public void GatherPikmins()
		{
			_pikminControlComponent.SetTargetToPikmins(gameObject, true);
			_gatherSfxComponent.Play();
			_whistleDelay = 0.5f;
		}

		private void OnDestroy()
		{
			_disposable.Dispose();
		}
	}
}