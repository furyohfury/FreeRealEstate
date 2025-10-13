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
			_moveComponent.MoveTo(point);
		}

		public void StopMovement()
		{
			_moveComponent.MoveTo(_moveComponent.Position);
		}
	}
}