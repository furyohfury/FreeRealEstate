using UnityEngine;

namespace GameDebug
{
	public sealed class SleepThresholdDebug : MonoBehaviour
	{
		private void Start()
		{
			GetComponent<Rigidbody>().sleepThreshold = 0f;
		}
	}
}