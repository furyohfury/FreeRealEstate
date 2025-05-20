using System;
using Game;
using UnityEngine;

namespace GameEngine
{
	[Serializable]
	public sealed class MoveRigidbodyComponent
	{
		public AndCondition CanMove = new();
		public Vector3 Position => _rigidbody.position;
		
		[SerializeField]
		private Rigidbody _rigidbody;
		[SerializeField]
		private Transform _transform;
		[SerializeField]
		private float _moveSpeed;

		public void Move(Vector3 direction)
		{
			if (direction == Vector3.zero || CanMove.Invoke())
			{
				return;
			}
			
			var delta = _transform.forward * direction.z + _transform.right * direction.x;
			_rigidbody.MovePosition(_transform.position + delta * _moveSpeed);
		}

		public void MoveTo(Vector3 position)
		{
			_rigidbody.MovePosition(position);
		}
	}
}