using UnityEngine;
using VContainer.Unity;

namespace Game.App
{
	public sealed class FramerateController : IInitializable
	{
		public void Initialize()
		{
#if UNITY_ANDROID
			Application.targetFrameRate = 120;
#endif
		}
	}
}