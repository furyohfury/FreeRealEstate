using System;
using R3;
using UnityEngine;

namespace GameEngine
{
	[Serializable]
	public sealed class LifeComponent
	{
		public int MaxHealth => _maxHealth;
		public ReadOnlyReactiveProperty<int> CurrentHealth => _currentHealth;
		public bool IsAlive => _currentHealth.Value > 0;

		[SerializeField]
		private int _maxHealth;
		[SerializeField]
		private SerializableReactiveProperty<int> _currentHealth = new SerializableReactiveProperty<int>(5);

		public void ChangeHealth(int delta)
		{
			_currentHealth.Value = Mathf.Clamp(_currentHealth.Value + delta, 0, _maxHealth);
		}
	}
}