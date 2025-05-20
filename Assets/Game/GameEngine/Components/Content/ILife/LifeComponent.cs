using System;
using UnityEngine;

namespace GameEngine
{
	[Serializable]
	public sealed class LifeComponent
	{
		public int MaxHealth => _maxHealth;
		public int CurrentHealth => _currentHealth;
		public bool IsAlive => _currentHealth > 0;

		[SerializeField]
		private int _maxHealth;
		[SerializeField]
		private int _currentHealth;

		public void ChangeHealth(int delta)
		{
			_currentHealth = Mathf.Clamp(_currentHealth + delta, 0, _maxHealth);
		}
	}
}