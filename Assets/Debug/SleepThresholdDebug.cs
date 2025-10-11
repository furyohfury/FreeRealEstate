using UnityEngine;

namespace Debug
{
	public sealed class SleepThresholdDebug : MonoBehaviour
	{
		private void Start()
		{
			GetComponent<Rigidbody>().sleepThreshold = 0f;
		}
	}
}