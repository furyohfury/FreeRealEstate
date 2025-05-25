using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine
{
	[Serializable]
	public class CarriableComponent
	{
		[ShowInInspector]
		public bool IsCarried => _currentForce >= _weight;
		
		[SerializeField]
		private int _weight;
		[SerializeField]
		private float _movingSpeed;
		[SerializeField]
		private Transform _transform;
		[ShowInInspector] [ReadOnly]
		private int _currentForce;
		[ShowInInspector]
		private HashSet<Transform> _carriers = new();

		[Button]
		public bool AddCarrier(Transform transform, int force)
		{
			if (IsCarried || _carriers.Add(transform) == false)
			{
				return false;
			}

			_currentForce += force;
			return true;
		}

		[Button]
		public void RemoveCarrier(Transform transform, int force)
		{
			if (_carriers.Remove(transform) == false)
			{
				throw new NullReferenceException($"No such carrier as {transform.gameObject.name}");
			}

			_currentForce -= force;
		}

		public void ClearCarriers()
		{
			_carriers.Clear();
			_currentForce = 0;
		}

		public void Update(float deltaTime)
		{
			if (IsCarried == false)
			{
				return;
			}

			Vector3 averagePos = Vector3.zero;
			foreach (var carrier in _carriers)
			{
				averagePos += carrier.transform.position; // TODO make with event so facade would move with movecomponent
			}

			_transform.position = Vector3.Lerp(_transform.position, averagePos / _carriers.Count, deltaTime * _movingSpeed);
		}
	}
}