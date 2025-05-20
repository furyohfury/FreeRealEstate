using System;
using Game;
using UnityEngine;

namespace GameEngine
{
	[Serializable]
	public sealed class MoveCharControllerComponent
	{
		public AndCondition CanMove = new();

		[SerializeField]
		private CharacterController _characterController;
		[SerializeField]
		private float _speed;

		public void Move(Vector3 direction)
		{
			if (CanMove.Invoke() == false)
			{
				return;
			}

			_characterController.SimpleMove(direction * _speed);
		}
	}
}