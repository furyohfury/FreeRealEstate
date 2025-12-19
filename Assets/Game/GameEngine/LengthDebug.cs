using UnityEngine;

namespace GameEngine
{
	public sealed class LengthDebug : MonoBehaviour
	{
		[SerializeField]
		private float _length;
		[SerializeField]
		private float _sphereRadius;

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.yellow;
			Vector3 endPos = transform.position - transform.up * _length;
			Gizmos.DrawLine(transform.position, endPos);
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(endPos, _sphereRadius);
		}
	}
}
