using GameEngine;
using UnityEngine;

namespace Game
{
	public sealed class Player : MonoBehaviour,
		IHitPoints,
		IChangeHealth,
		IMoveable,
		IRotateable
	{
		public int HitPoints => _lifeComponent.CurrentHealth;

		public Vector3 Position => transform.position;

		[SerializeField]
		private MoveCharControllerComponent _moveCharControllerComponent;
		[SerializeField]
		private LifeComponent _lifeComponent;
		[SerializeField]
		private RotateTransformComponent _rotateTransformComponent;

		private void Awake()
		{
			_moveCharControllerComponent.CanMove.AddCondition(() => _lifeComponent.IsAlive);
			_rotateTransformComponent.CanRotate.AddCondition(() => _lifeComponent.IsAlive);
		}

		public void ChangeHealth(int delta)
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
	}
}