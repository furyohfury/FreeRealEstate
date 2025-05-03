using System;
using Game;
using UnityEngine;

namespace GameEngine
{
	[Serializable]
	public sealed class RotateComponent : IComponent
	{
		public AndCondition CanRotate = new();
		[SerializeField]
		private float _rotateSpeed;
		[SerializeField]
		private Rigidbody _rigidbody;

		public void Rotate(Vector3 delta)
		{
			if (CanRotate.Invoke() == false)
			{
				return;
			}
			
			var rotation = _rigidbody.rotation;
			var smoothedDirection = Quaternion.Lerp(rotation, rotation * Quaternion.Euler(delta), _rotateSpeed);
			_rigidbody.MoveRotation(smoothedDirection);
		}

		public void RotateTo(Quaternion direction)
		{
			if (CanRotate.Invoke() == false)
			{
				return;
			}

			var smoothedRotation = Quaternion.Lerp(_rigidbody.rotation, direction, _rotateSpeed);
			_rigidbody.MoveRotation(smoothedRotation);
		}

		public void RotateTo(Vector3 direction)
		{
			var quaternion = Quaternion.Euler(direction);
			RotateTo(quaternion);
		}
	}
}