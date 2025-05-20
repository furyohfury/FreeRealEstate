using System;
using Game;
using UnityEngine;

namespace GameEngine
{
	[Serializable]
	public sealed class RotateTransformComponent
	{
		public AndCondition CanRotate = new();
		
		[SerializeField]
		private float _rotateSpeed;
		[SerializeField]
		private Transform _transform;
		
		public void Rotate(Vector3 delta)
		{
			if (CanRotate.Invoke() == false)
			{
				return;
			}
			
			var rotation = _transform.rotation;
			var smoothedDirection = Quaternion.Lerp(rotation, rotation * Quaternion.Euler(delta), _rotateSpeed);
			_transform.rotation = smoothedDirection;
		}

		public void RotateTo(Quaternion rotation)
		{
			if (CanRotate.Invoke() == false)
			{
				return;
			}

			_transform.rotation = rotation;
		}
	}
}