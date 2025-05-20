using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine
{
	[Serializable]
	public sealed class CarryComponent
	{
		public int Strength => _strength;

		[SerializeField]
		private int _strength;
		[SerializeField]
		private Transform _transform;

		[Button]
		public bool TryCarry(GameObject entity)
		{
			return entity.TryGetComponent(out ICarriable carriable)
			       && carriable.AddCarrier(_transform, _strength);
		}
	}
}