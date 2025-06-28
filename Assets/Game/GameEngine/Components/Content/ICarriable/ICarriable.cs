using UnityEngine;

namespace GameEngine
{
	public interface ICarriable
	{
		bool IsCarried { get; }
		bool AddCarrier(Transform transform, int force, out Transform anchorPoint);
		void RemoveCarrier(Transform transform, int force);
		void ClearCarriers();
	}
}