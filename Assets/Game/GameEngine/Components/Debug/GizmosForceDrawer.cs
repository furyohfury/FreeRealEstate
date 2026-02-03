using UnityEngine;

namespace GameEngine
{
	public sealed class GizmosForceDrawer : MonoBehaviour
	{
		public Vector3 Direction = Vector3.zero;
		public float Divider = 10;

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawLine(transform.position, transform.position + Direction / Divider);
		}
	}
}
