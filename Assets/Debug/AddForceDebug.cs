using Sirenix.OdinInspector;
using UnityEngine;

namespace Debug
{
	public class AddForceDebug : MonoBehaviour
	{
		public Vector3 Force;
		public Rigidbody rb;

		[Button]
		private void AddForce()
		{
			rb.AddForce(Force, ForceMode.Impulse);
		}

		[Button]
		private void SetVelocity()
		{
			rb.linearVelocity = Force;
		}
	}
}