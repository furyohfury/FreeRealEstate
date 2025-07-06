using UnityEngine;

namespace Game
{
	public class CollisionDebug : MonoBehaviour
	{
		[SerializeField]
		private Collider _collider;

		private void OnTriggerEnter(Collider other)
		{
			UnityEngine.Debug.Log("triggered");

			if (other.TryGetComponent(out ComplexCarriableEntity _))
			{
				UnityEngine.Debug.Log("with CarriableEntityDebug");
			}
			else
			{
				UnityEngine.Debug.Log("but didnt get component");
			}
		}
	}
}