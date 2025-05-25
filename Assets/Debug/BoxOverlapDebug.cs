using UnityEngine;

namespace Game
{
	public class BoxOverlapDebug : MonoBehaviour
	{
		[SerializeField]
		private Transform _transform;
		[SerializeField]
		private Vector3 _size = new Vector3(4, 10, 4);
		
		private void OnDrawGizmos()
		{
			if (_transform == null) return;

			Gizmos.color = Color.red;
			Gizmos.matrix = Matrix4x4.TRS(_transform.position, Quaternion.identity, Vector3.one);
			Gizmos.DrawWireCube(Vector3.zero, _size);
		}
	}
}