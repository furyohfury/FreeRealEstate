using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine
{
	[Serializable]
	public sealed class CarryComponent
	{
		public int Strength => _strength;
		public Transform AnchorPoint => _anchorPoint;

		[ShowInInspector]
		private ICarriable _activeCarriable;
		[SerializeField]
		private int _strength;
		[SerializeField]
		private Transform _transform;
		private Transform _anchorPoint;

		[Button]
		public bool TryCarry(GameObject entity)
		{
			Transform anchorPoint = null;
			bool canCarry = entity.TryGetComponent(out ICarriable carriable)
			                && carriable.AddCarrier(_transform, _strength, out anchorPoint);
			if (canCarry)
			{
				_anchorPoint = anchorPoint;
				_activeCarriable = carriable;
			}

			return canCarry;
		}

		public void StopCarry()
		{
			if (_activeCarriable != null)
			{
				_activeCarriable.RemoveCarrier(_transform, _strength);
				_activeCarriable = null;
			}
		}
	}
}