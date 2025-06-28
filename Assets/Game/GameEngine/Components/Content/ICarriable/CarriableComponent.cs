using System;
using System.Collections.Generic;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine
{
	[Serializable]
	public class CarriableComponent
	{
		[ShowInInspector]
		public bool IsCarried => _currentForce.Value >= _weight;

		public int Weight => _weight;
		public ReadOnlyReactiveProperty<int> CurrentForce => _currentForce;

		[SerializeField]
		private int _weight;
		[SerializeField]
		private float _movingSpeed;
		[SerializeField]
		private Transform _transform;
		[SerializeField]
		private Transform[] _carryAnchorPoints;
		[ShowInInspector] [ReadOnly]
		private ReactiveProperty<int> _currentForce = new();
		[ShowInInspector] [ReadOnly]
		private HashSet<Transform> _carriers = new();

		private Transform[] _carriersOnPoints;

		[Button]
		public bool AddCarrier(Transform carrier, int force, out Transform freeAnchorPoint)
		{
			if (_carriersOnPoints == null)
			{
				_carriersOnPoints = new Transform[_carryAnchorPoints.Length];
			}

			if (IsCarried || _carriers.Add(carrier) == false)
			{
				freeAnchorPoint = null;
				return false;
			}

			freeAnchorPoint = GetFreeAnchorPoint(carrier);

			_currentForce.Value += force;
			return true;
		}

		[Button]
		public void RemoveCarrier(Transform carrier, int force)
		{
			if (_carriers.Remove(carrier) == false)
			{
				return;
			}

			var carrierIndex = Array.IndexOf(_carriersOnPoints, carrier);
			_carriersOnPoints[carrierIndex] = null;
			_currentForce.Value -= force;
		}

		public void ClearCarriers()
		{
			_carriers.Clear();
			_currentForce.Value = 0;
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
				averagePos += carrier.transform.position;
			}

			averagePos /= _carriers.Count;
			averagePos.y = _transform.position.y;
			_transform.position = Vector3.Lerp(_transform.position, averagePos, deltaTime * _movingSpeed);
		}

		private Transform GetFreeAnchorPoint(Transform carrier)
		{
			for (int i = 0, count = _carriersOnPoints.Length; i < count; i++)
			{
				if (_carriersOnPoints[i] == null)
				{
					_carriersOnPoints[i] = carrier;
					return _carryAnchorPoints[i];
				}
			}

			return null;
		}
	}
}