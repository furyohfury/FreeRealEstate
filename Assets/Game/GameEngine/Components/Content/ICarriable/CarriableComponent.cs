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
		[ShowInInspector] [ReadOnly]
		private ReactiveProperty<int> _currentForce = new();
		[ShowInInspector]
		private HashSet<Transform> _carriers = new();

		[Button]
		public bool AddCarrier(Transform transform, int force)
		{
			if (IsCarried || _carriers.Add(transform) == false)
			{
				return false;
			}

			_currentForce.Value += force;
			return true;
		}

		[Button]
		public void RemoveCarrier(Transform transform, int force)
		{
			if (_carriers.Remove(transform) == false)
			{
				return;
			}

			_currentForce.Value -= force;
		}

		public void ClearCarriers()
		{
			_carriers.Clear();
			_currentForce.Value = 0;
		}

		public void Update(float deltaTime)
		{
			MoveAmongCarriers(deltaTime);
		}

		private void MoveAmongCarriers(float deltaTime)
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

			averagePos /= _carriers.Count;
			averagePos.y = _transform.position.y;
			_transform.position = Vector3.Lerp(_transform.position, averagePos, deltaTime * _movingSpeed);
		}
	}
}