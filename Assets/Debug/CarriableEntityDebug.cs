using GameEngine;
using UnityEngine;

namespace Game.Debug
{
	public sealed class CarriableEntityDebug : MonoBehaviour,
		IHitPoints,
		IChangeHealth,
		ICarriable,
		IIdentifier,
		IMoveable,
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
		
		public void ChangeHealth(int delta)
		{
			_lifeComponent.ChangeHealth(delta);
			if (_lifeComponent.IsAlive == false)
			{
				
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

		private void OnDestroy()
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
	}
}