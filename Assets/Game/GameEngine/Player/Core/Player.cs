using GameEngine;
using UnityEngine;

namespace Game
{
	public sealed class Player : Entity
	{
		[SerializeField]
		private MoveComponent _moveComponent;
		[SerializeField]
		private LifeComponent _lifeComponent;
		[SerializeField]
		private RotateComponent _rotateComponent;

		private void Awake()
		{
			_moveComponent.CanMove.AddCondition(() => _lifeComponent.IsAlive);
			_rotateComponent.CanRotate.AddCondition(() => _lifeComponent.IsAlive);
			
			AddComponent(_moveComponent);
			AddComponent(_lifeComponent);
			AddComponent(_rotateComponent);
		}
	}
}