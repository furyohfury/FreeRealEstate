using GameEngine;
using R3;
using UnityEngine;

namespace Game
{
	public sealed class SimpleCarriableEntityDebug : MonoBehaviour,
		IHitPoints,
		ITakeDamage,
		ICarriable,
		IIdentifier,
		IMoveable,
		IPikminInteractable,
		IDestroyable
	{
		public string Id => _idComponent.ID;

		public int HitPoints => _lifeComponent.CurrentHealth.CurrentValue;
		public bool IsCarried => _carriableComponent.IsCarried;
		public Vector3 Position => transform.position;

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

		[SerializeField] [Header("UI")]
		private HealthbarUIComponent _healthbarUI;
		[SerializeField]
		private CarriableUIComponent _carriableUIComponent;

		private readonly CompositeDisposable _disposable = new();

		private void Start()
		{
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

			_carriableComponent.CurrentForce
			                   .Subscribe(force =>
				                   _carriableUIComponent.SetAmount(
					                   force,
					                   _carriableComponent.Weight)
			                   )
			                   .AddTo(_disposable);
		}

		private void Update()
		{
			_carriableComponent.Update(Time.deltaTime);
		}

		public void TakeDamage(int delta)
		{
			_lifeComponent.ChangeHealth(delta);
		}

		public bool AddCarrier(Transform carrier, int force, out Transform anchorPoint)
		{
			return _carriableComponent.AddCarrier(carrier, force, out anchorPoint);
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