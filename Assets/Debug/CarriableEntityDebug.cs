	using System;
	using GameEngine;
	using UnityEngine;

	namespace Game.Debug
{
	public sealed class CarriableEntityDebug : Entity
	{
		[SerializeField]
		private CarriableComponent _carriableComponent;
		[SerializeField]
		private MoveRigidbodyComponent _moveRigidbodyComponent;
		[SerializeField]
		private DestroyComponent _destroyComponent;
		[SerializeField]
		private PointsValueComponent _pointsValueComponent;

		private void Awake()
		{
			AddComponent(_carriableComponent);
			AddComponent(_moveRigidbodyComponent);
			AddComponent(_destroyComponent);
			AddComponent(_pointsValueComponent);
		}
	}
}