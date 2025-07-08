using UnityEngine;

namespace Game
{
	public interface IPikminTarget
	{
		bool TrySetTarget(GameObject target);
		GameObject Target { get; }
	}
}