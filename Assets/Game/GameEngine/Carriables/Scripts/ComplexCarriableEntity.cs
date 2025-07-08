using System;
using GameEngine;
using R3;
using UnityEngine;

namespace Game
{
	[SelectionBase]
	public sealed class ComplexCarriableEntity : MonoBehaviour,
		IHitPoints,
		ITakeDamage,
		IGetAnchorPoint,
		ICarriable,
		IIdentifier,
		IMoveable,
		IPikminInteractable,
		IDestroyable
	{
		public string Id => _idComponent.ID;

		public Observable<GameObject> OnDead => _destroyComponent.OnDead;
		public int HitPoints => _lifeComponent.CurrentHealth.CurrentValue;
		public bool IsCarried => _carriableComponent.IsCarried;
		public Vector3 Position => transform.position;

		[SerializeField]
		private AnchorPointsComponent _anchorPointsComponent;
		[SerializeField]
		private CarriableComponent _carriableComponent;
		[SerializeField]
		private DestroyComponent _destroyComponent;
		[SerializeField]
		private IdComponent _idComponent;
		[SerializeField]
		private MoveTransformComponent _moveTransformComponent;
		[SerializeField]
		private LifeComponent _lifeComponent;
		[SerializeField]
		private TakeDamageStateChangeComponent _damageStateChangeComponent;

		[SerializeField] [Header("UI")]
		private HealthbarUIComponent _healthbarUI;
		[SerializeField]
		private CarriableUIComponent _carriableUIComponent;

		[Header("Effects")]
		[SerializeField]
		private DestroySFXComponent _destroySfxComponent;
		[SerializeField]
		private StateChangeVFXComponent _stateChangeVFXComponent;
		[SerializeField]
		private float[] _effectPlayHpThresholds;

		private readonly CompositeDisposable _disposable = new();


		private void Start()
		{
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
				              _healthbarUI.SetRatio((float)hp / _lifeComponent.MaxHealth);
				              if (hp <= 0)
				              {
					              _healthbarUI.Hide();
				              }
			              })
			              .AddTo(_disposable);

			_lifeComponent.CurrentHealth
			              .Where(hp => hp <= 0)
			              .Take(1)
			              .Subscribe(_ => _damageStateChangeComponent.ChangeState())
			              .AddTo(_disposable);

			if (_effectPlayHpThresholds != null)
			{
				for (int i = 0, count = _effectPlayHpThresholds.Length; i < count; i++)
				{
					var index = i;
					_lifeComponent.CurrentHealth
					              .Where(hp => hp <= _lifeComponent.MaxHealth * _effectPlayHpThresholds[index])
					              .Take(1)
					              .Subscribe(_ =>
					              {
						              _stateChangeVFXComponent.PlayVFX();
						              _destroySfxComponent.PlaySFX();
					              })
					              .AddTo(_disposable);
				}
			}
		}

		private void Update()
		{
			_carriableComponent.Update(Time.deltaTime);
		}

		public Transform GetAnchorPoint()
		{
			return _anchorPointsComponent.GetAnchorPoint();
		}

		public void TakeDamage(int delta)
		{
			_lifeComponent.ChangeHealth(delta);
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

		public void Move(Vector3 direction)
		{
			_moveTransformComponent.Move(direction);
		}

		public void MoveTo(Vector3 position)
		{
			_moveTransformComponent.MoveTo(position);
		}

		public void Destroy()
		{
			_destroyComponent.Destroy();
		}

		private void OnDestroy()
		{
			_carriableComponent.ClearCarriers();
			_disposable.Dispose();
		}
	}
}