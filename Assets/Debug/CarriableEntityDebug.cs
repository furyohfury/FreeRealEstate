using System;
using GameEngine;
using UnityEngine;

namespace Game
{
	public sealed class CarriableEntityDebug : MonoBehaviour,
		IHitPoints,
		ITakeDamage,
		ICarriable,
		IIdentifier,
		IMoveable,
		IPikminInteractable,
		IDestroyable
	{
		public string Id => _idComponent.ID;

		public int HitPoints => _lifeComponent.CurrentHealth;
		public bool IsCarried => _carriableComponent.IsCarried;

		[SerializeField]
		private CarriableComponent _carriableComponent;
		[SerializeField]
		private DestroyCompositeComponent _destroyComponent;
		[SerializeField]
		private IdComponent _idComponent;
		[SerializeField]
		private MoveTransformComponent _moveTransformComponent;
		[SerializeField]
		private LifeComponent _lifeComponent;

		private void Update()
		{
			_carriableComponent.Update(Time.deltaTime);
		}

		public void TakeDamage(int delta)
		{
			_lifeComponent.ChangeHealth(delta);
			if (_lifeComponent.IsAlive == false)
			{
				Destroy();
			}
		}

		public bool AddCarrier(Transform transform, int force)
		{
			return _carriableComponent.AddCarrier(transform, force);
		}

		public void RemoveCarrier(Transform transform, int force)
		{
			_carriableComponent.RemoveCarrier(transform, force);
		}

		public void ClearCarriers()
		{
			_carriableComponent.ClearCarriers();
		}

		public Vector3 Position => transform.position;


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
		}
	}
}