using System;
using UnityEngine;

namespace GameEngine
{
	[Serializable]
	public sealed class MoveTransformComponent
	{
		public Vector3 Position => _transform.position;
		
		[SerializeField]
		private Transform _transform;
		[SerializeField]
		private float _speed = 0f;

		public void Move(Vector3 direction)
		{
			_transform.Translate(direction * _speed * Time.deltaTime);
		}

		public void MoveTo(Vector3 position)
		{
			_transform.position = position;
		}
	}
}