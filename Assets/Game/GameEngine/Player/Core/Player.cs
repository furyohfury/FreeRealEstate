using System;
using GameEngine;
using UnityEngine;

namespace Game
{
	public sealed class Player : Entity
	{
		[SerializeField]
		private MoveRigidbodyComponent _moveRigidbodyComponent;
		[SerializeField]
		private LifeComponent _lifeComponent;
		[SerializeField]
		private RotateComponent _rotateComponent;
		[SerializeField]
		private AttackComponent _attackComponent;

		private void Awake()
		{
			_moveRigidbodyComponent.CanMove.AddCondition(() => _lifeComponent.IsAlive);
			_rotateComponent.CanRotate.AddCondition(() => _lifeComponent.IsAlive);
			_attackComponent.CanAttack.AddCondition(() => _lifeComponent.IsAlive);

			AddComponent(_moveRigidbodyComponent);
			AddComponent(_lifeComponent);
			AddComponent(_rotateComponent);
			AddComponent(_attackComponent);
		}
	}
}