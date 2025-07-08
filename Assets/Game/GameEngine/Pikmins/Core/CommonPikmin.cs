using System;
using GameEngine;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
	public sealed class CommonPikmin : // TODO bind navmesh and animation with isalive too
		MonoBehaviour,
		IHitPoints,
		ITakeDamage,
		ICarry,
		IAttackable,
		IPikminTarget,
		IRotateable,
		IDestroyable
	{
		public int HitPoints => _lifeComponent.CurrentHealth.CurrentValue;
		public Observable<GameObject> OnDead => _destroyComponent.OnDead;

		[ShowInInspector]
		public GameObject Target => _target;

		[SerializeField]
		private LifeComponent _lifeComponent;
		[SerializeField]
		private CarryComponent _carryComponent;
		[SerializeField]
		private AttackComponent _attackComponent;
		[SerializeField]
		private NavMeshComponent _navMeshComponent;
		[SerializeField]
		private AnimatorComponent _animatorComponent;
		[SerializeField]
		private RotateTransformComponent _rotateComponent;
		[SerializeField] 
		private DestroyComponent _destroyComponent;

		[SerializeField] [Header("Effects")]
		private AttackSFXComponent _attackSfxComponent;
		[SerializeField]
		private MoveSFXComponent _moveSfxComponent;
		[SerializeField] 
		private DestroySFXComponent _destroySfxComponent;
		[SerializeField]
		private DestroyVFXComponent _destroyVFXComponent;

		private GameObject _target;
		private readonly CompositeDisposable _disposable = new();


		private void Awake()
		{
			_attackComponent.CanAttack.AddCondition(() => _lifeComponent.IsAlive);
		}

		private void Start()
		{
			_attackComponent.Initialize();
			_attackComponent.OnAttacked
			                .Subscribe(_ => _attackSfxComponent.Play())
			                .AddTo(_disposable);

			Observable.EveryUpdate()
			          .Subscribe(_ =>
			          {
				          var isMoving = _navMeshComponent.Velocity != Vector3.zero;
				          AnimateMovement(isMoving);
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
			
			_lifeComponent.CurrentHealth
			              .Where(hp => hp <= 0)
			              .Take(1)
			              .Subscribe(_ =>
			              {
				              _destroyVFXComponent.PlayVFX();
				              _destroySfxComponent.PlaySFX();
				              _destroyComponent.Destroy();
			              })
			              .AddTo(_disposable);
		}

		public bool TrySetTarget(GameObject target)
		{
			if (target.TryGetComponent(out IPikminInteractable _))
			{
				_target = target;
				return true;
			}

			return false;
		}

		private void AnimateMovement(bool isMoving)
		{
			_animatorComponent.Animator.SetBool(AnimatorHash.IsMoving, isMoving);
		}

		public void TakeDamage(int delta)
		{
			_lifeComponent.ChangeHealth(delta);
		}

		public bool TryCarry(GameObject target)
		{
			return _carryComponent.TryCarry(target);
		}

		public void StopCarry()
		{
			_carryComponent.StopCarry();
		}

		public void Attack()
		{
			_attackComponent.Attack();
		}

		public void Rotate(Vector3 delta)
		{
			_rotateComponent.Rotate(delta);
		}

		public void RotateTo(Quaternion direction)
		{
			_rotateComponent.RotateTo(direction);
		}

		private void OnDestroy()
		{
			_attackComponent.Dispose();
			_disposable.Dispose();
		}

		public void Destroy()
		{
			_destroyComponent.Destroy();
		}
	}
}