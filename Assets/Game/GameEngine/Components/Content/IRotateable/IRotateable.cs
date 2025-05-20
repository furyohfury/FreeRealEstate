using UnityEngine;

namespace GameEngine
{
	public interface IRotateable
	{
		void Rotate(Vector3 delta);
		void RotateTo(Quaternion direction);
	}
}