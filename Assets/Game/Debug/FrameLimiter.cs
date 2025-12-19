using UnityEngine;

namespace Game.Debug
{
	public class FrameLimiter : MonoBehaviour
	{
		private void Start()
		{
			Application.targetFrameRate = 60;
		}
	}
}
