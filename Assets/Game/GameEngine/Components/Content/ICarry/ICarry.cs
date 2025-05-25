using UnityEngine;

namespace GameEngine
{
	public interface ICarry
	{
		bool TryCarry(GameObject entity);

		void StopCarry();
	}
}