using GameEngine;
using UnityEngine;

namespace Game.Debug
{
	public sealed class CarriableEntityDebug : MonoBehaviour,
		ICarriable,
		IIdentifier,
		IMoveable,
		IDestroyable
	{
		public string Id => _idComponent.ID;

		[SerializeField]
		private CarriableComponent _carriableComponent;
		[SerializeField]
		private DestroyComponent _destroyComponent;
		[SerializeField]
		private IdComponent _idComponent;
		[SerializeField]
		private MoveTransformComponent _moveTransformComponent;

		public bool IsCarried => _carriableComponent.IsCarried;

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