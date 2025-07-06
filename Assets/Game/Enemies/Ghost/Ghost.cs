using System;
using GameEngine;
using R3;
using UnityEngine;

namespace Game
{
	[SelectionBase]
	public sealed class Ghost :
		MonoBehaviour,
		IHitPoints,
		ITakeDamage,
		IGetAnchorPoint,
		ICarriable,
		IIdentifier,
		IAttackable,
		IRotateable,
		IPikminInteractable,
		IDestroyable
	{
		public Observable<GameObject> OnDead => _destroyComponent.OnDead;
		public string Id => _id;
		public int CurrentHealth => _lifeComponent.CurrentHealth.CurrentValue;
		public bool IsCarried => _carriableComponent.IsCarried;
		public int HitPoints => _lifeComponent.CurrentHealth.CurrentValue;

		[SerializeField]
		private string _id;
		[SerializeField]
		private LifeComponent _lifeComponent;
		[SerializeField]
		private AttackComponent _attackComponent;
		[SerializeField]
		private NavMeshComponent _navMeshComponent;
		[SerializeField]
		private AnimatorComponent _animatorComponent;
		[SerializeField]
		private RotateTransformComponent _rotateComponent;
		[SerializeField]
		private AnchorPointsComponent _anchorPointsComponent;
		[SerializeField]
		private CarriableComponent _carriableComponent;
		[SerializeField]
		private DestroyComponent _destroyComponent;

		[SerializeField] [Header("Effects")]
		private AttackSFXComponent _attackSfxComponent;
		[SerializeField]
		private MoveSFXComponent _moveSfxComponent;
		[Header("UI")]
		[SerializeField]
		private HealthbarUIComponent _healthbarUIComponent;
		[SerializeField]
		private CarriableUIComponent _carriableUIComponent;

		private readonly CompositeDisposable _disposable = new();


		private void Awake()
		{
			_attackComponent.CanAttack.AddCondition(() => _lifeComponent.IsAlive);
			_carriableUIComponent.Hide();
		}

		private void Start()
		{
			_attackComponent.Initialize();
			_attackComponent.OnAttacked
			                .Subscribe(_ => _attackSfxComponent.PlaySFX())
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
				          
				          _carriableComponent.Update(Time.deltaTime);
			          })
			          .AddTo(_disposable);

			_lifeComponent.CurrentHealth
			              .Where(hp => hp <= 0)
			              .Take(1)
			              .Subscribe(_ =>
			              {
				              _navMeshComponent.Disable();
				              _animatorComponent.Animator.SetTrigger(AnimatorHash.Death);
				              _carriableUIComponent.Show();
				              _healthbarUIComponent.Hide();
			              })
			              .AddTo(_disposable);

			_carriableComponent.CurrentForce
			                   .Subscribe(force =>
				                   _carriableUIComponent.SetAmount(
					                   force,
					                   _carriableComponent.Weight)
			                   )
			                   .AddTo(_disposable);
			
			_lifeComponent.CurrentHealth
			              .Subscribe(hp =>
			              {
				              _healthbarUIComponent.SetRatio((float)hp / _lifeComponent.MaxHealth);
			              })
			              .AddTo(_disposable);
		}

		private void AnimateMovement(bool isMoving)
		{
			_animatorComponent.Animator.SetBool(AnimatorHash.IsMoving, isMoving);
		}

		public void TakeDamage(int delta)
		{
			_lifeComponent.ChangeHealth(delta);
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

		public Transform GetAnchorPoint()
		{
			return _anchorPointsComponent.GetAnchorPoint();
		}

		public bool AddCarrier(Transform carrier, int force)
		{
			return _carriableComponent.AddCarrier(carrier, force);
		}

		public void RemoveCarrier(Transform carrier, int force)
		{
			_carriableComponent.RemoveCarrier(carrier, force);
		}

		public void ClearCarriers()
		{
			_carriableComponent.ClearCarriers();
		}

		public void Destroy()
		{
			_destroyComponent.Destroy();
		}

		private void OnDestroy()
		{
			_attackComponent.Dispose();
			_disposable.Dispose();
		}
	}
}