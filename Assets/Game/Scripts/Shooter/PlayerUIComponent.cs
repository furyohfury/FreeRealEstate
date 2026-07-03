using System;
using Unity.Netcode;
using UnityEngine;

namespace Game.Scripts.Shooter
{
    [Serializable]
    public sealed class PlayerUIComponent
    {
        [SerializeField]
        private PlayerUI _playerUI;
        private NetworkVariable<float> _health;

        public void Init(NetworkVariable<float> health)
        {
            _health = health;
            _health.OnValueChanged += OnValueChanged;
            SetHPText(_health.Value);
        }

        private void OnValueChanged(float previousValue, float newValue)
        {
            SetHPText(newValue);
        }

        private void SetHPText(float newValue)
        {
            _playerUI.SetText(newValue.ToString());
        }

        public void Dispose()
        {
            _health.OnValueChanged -= OnValueChanged;
        }

        public void TurnHpUiTo(Vector3 direction)
        {
            _playerUI.TurnTo(direction);
        }
    }
}
