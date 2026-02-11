using System;
using UnityEngine;

namespace Game
{
    public sealed class Health : MonoBehaviour
    {
        public event Action<float> OnHealthChanged;

        public float CurrentHealth
        {
            get => _currentHealth;
            set
            {
                _currentHealth = Mathf.Clamp(value, 0, MaxHealth);
                OnHealthChanged?.Invoke(_currentHealth);
            }
        }
        [field: SerializeField]
        public float MaxHealth { get; private set; }

        [SerializeField]
        private float _currentHealth;
    }
}
