using Unity.Netcode;
using UnityEngine;

namespace Gameplay
{
	public sealed class Bat : MonoBehaviour
	{
		public Vector3 Position => _moveComponent.Position;

		public NetworkObject NetworkObject => _networkObject;

		[SerializeField]
		private MoveComponent _moveComponent;

		[SerializeField]
		private NetworkObject _networkObject;

		public void MoveTo(Vector3 point)
		{
			_moveComponent.SetDestination(point);
		}

		public void MoveInstantly(Vector3 point)
		{
			_moveComponent.MoveInstantly(point);
		}

		public void StopMovement()
		{
			_moveComponent.SetDestination(Position);
		}

		public void DisableMovement()
		{
			StopMovement();
			_moveComponent.Disable();
		}
	}
}