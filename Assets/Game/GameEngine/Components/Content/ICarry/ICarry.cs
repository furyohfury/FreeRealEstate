using UnityEngine;

namespace GameEngine
{
	public interface ICarry
	{
		bool TryCarry(GameObject target);

		void StopCarry();
	}
}