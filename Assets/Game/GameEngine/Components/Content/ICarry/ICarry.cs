using UnityEngine;

namespace GameEngine
{
	public interface ICarry
	{
		Transform GetAnchorPoint();

		bool TryCarry(GameObject entity);

		void StopCarry();
	}
}