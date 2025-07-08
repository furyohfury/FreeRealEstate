using R3;
using UnityEngine;

namespace GameEngine
{
	public interface IObjectDetector
	{
		Subject<GameObject> OnEntityDetected { get; }
	}
}