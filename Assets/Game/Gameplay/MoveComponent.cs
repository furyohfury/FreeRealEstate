using Unity.Netcode;
using UnityEngine;

namespace Gameplay
{
	public sealed class MoveComponent : NetworkBehaviour
	{
		public Vector3 Destination { get; private set; }
		public Vector3 Position => _rigidbody.position;

		[SerializeField]
		private Rigidbody _rigidbody;
		[SerializeField]
		private float _speed;

		private void Start()
		{
			Destination = Position;
		}

		public void SetDestination(Vector3 point)
		{
			Destination = point;
		}

		public void MoveInstantly(Vector3 point)
		{
			_rigidbody.position = point;
		}

		public void Disable()
		{
			_rigidbody.isKinematic = true;
		}

		private void FixedUpdate()
		{
			if (IsServer)
			{
				Move(Destination, Time.fixedDeltaTime);
			}
		}

		private void Move(Vector3 destination, float deltaTime)
		{
			Vector3 direction = destination - Position;
			Vector3 velocity = direction / deltaTime;

			velocity = Vector3.ClampMagnitude(velocity, _speed);

			_rigidbody.linearVelocity = velocity;
		}
	}
}