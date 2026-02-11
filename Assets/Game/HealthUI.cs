using TMPro;
using UnityEngine;

namespace Game
{
    public sealed class HealthUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _healthText;
        [SerializeField]
        private Health _health;

        private void OnEnable()
        {
            _health.OnHealthChanged += HealthOnOnHealthChanged;
        }

        private void HealthOnOnHealthChanged(float obj)
        {
            _healthText.text = obj.ToString();
        }

        private void OnDisable()
        {
            _health.OnHealthChanged -= HealthOnOnHealthChanged;
        }
    }
}
