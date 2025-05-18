using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine
{
	[Serializable]
	public sealed class CarryComponent : IComponent
	{
		public int Strength => _strength;

		[SerializeField]
		private int _strength;
		[SerializeField]
		private Transform _transform;

		[Button]
		public bool TryCarry(Entity entity)
		{
			return entity.TryGetComponent(out CarriableComponent carriableComponent)
			       && carriableComponent.AddCarrier(_transform, _strength);
		}
	}
}