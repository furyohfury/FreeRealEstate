using UnityEngine;

namespace Game.Visuals
{
	public abstract class ElementView : MonoBehaviour
	{
		public Vector3 GetPosition()
		{
			return transform.position;
		}
		
		public void Move(Vector3 pos)
		{
			transform.position = pos;
		}
	}
}