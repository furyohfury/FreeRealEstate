using UnityEngine;

namespace Game
{
	public sealed class GizmosSphereOnPlaceDrawer : MonoBehaviour
	{
		[SerializeField] 
		private float _radius = 1f;
		[SerializeField]
		private Color _color = Color.yellow;
		
		private void OnDrawGizmos()
		{
			Gizmos.color = _color;
			Gizmos.DrawWireSphere(transform.position, _radius);
		}
	}
}