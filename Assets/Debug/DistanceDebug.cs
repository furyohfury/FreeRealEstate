using UnityEngine;

namespace Game
{
	public sealed class DistanceDebug : MonoBehaviour
	{
		[SerializeField] 
		private float _radius = 1f;
		[SerializeField] 
		private float _distance = 1f;

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.yellow;
			var endPos = transform.position + transform.forward * _distance;
			Gizmos.DrawLine(transform.position, endPos);
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(endPos, _radius);
		}
	}
}