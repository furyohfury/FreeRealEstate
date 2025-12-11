using UnityEngine;

namespace Gameplay
{
	public class Puck : MonoBehaviour
	{
		[SerializeField]
		private Rigidbody _rigidbody;

		public void Move(Vector3 pos)
		{
			_rigidbody.position = pos;
		}

		public void StopMovement()
		{
			_rigidbody.linearVelocity = Vector3.zero;
		}
	}

}