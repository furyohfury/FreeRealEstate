using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine
{
	[Serializable]
	public sealed class CarryComponent
	{
		public int Strength => _strength;

		[ShowInInspector]
		private ICarriable _activeCarriable;
		[SerializeField]
		private int _strength;
		[SerializeField]
		private Transform _transform;

		[Button]
		public bool TryCarry(GameObject entity)
		{
			bool canCarry = entity.TryGetComponent(out ICarriable carriable)
			                      && carriable.AddCarrier(_transform, _strength);
			if (canCarry)
			{
				_activeCarriable = carriable;
			}
			
			return canCarry;
		}

		public void StopCarry()
		{
			_activeCarriable?.RemoveCarrier(_transform, _strength);
		}
	}
}