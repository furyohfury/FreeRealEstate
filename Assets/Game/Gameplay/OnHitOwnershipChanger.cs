using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

namespace Gameplay
{
	public sealed class OnHitOwnershipChanger : MonoBehaviour
	{
		[SerializeField]
		private NetworkObject _self;

		private void OnCollisionEnter(Collision other)
		{
			// if (other.gameObject.TryGetComponent(out NetworkObject networkObject))
			// {
			// 	NetworkTransform networkTransform;
			// 	_self.ChangeOwnership(networkObject.NetworkObjectId);
			// }
		}
	}
}