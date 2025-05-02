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

		public void Rotate(Vector3 direction)
		{
			if (CanRotate.Invoke() == false)
			{
				return;
			}
			
			var rotation = _rigidbody.rotation;
			_rigidbody.MoveRotation(Quaternion.Lerp(rotation, rotation * Quaternion.Euler(direction), _rotateSpeed));
		}
	}
}