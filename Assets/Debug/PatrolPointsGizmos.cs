using UnityEngine;

namespace Game
{
	public sealed class PatrolPointsGizmos : MonoBehaviour
	{
		[SerializeField]
		private Transform[] _points;

		private void OnDrawGizmos()
		{
			if (_points is not { Length: > 0 })
			{
				return;
			}
			Gizmos.color = Color.blue;
			for (int i = 0, count = _points.Length; i < count; i++)
			{
				Gizmos.DrawLine(_points[i].position, _points[(i + 1) % _points.Length].position);
			}
		}
	}
}