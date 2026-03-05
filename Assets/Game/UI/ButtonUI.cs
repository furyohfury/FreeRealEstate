using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public sealed class ButtonUI : MonoBehaviour
    {
        public event Action<ButtonUI> OnClick;
        
        [SerializeField]
        private Button _button;
        [SerializeField]
        private TextMeshProUGUI _text;

        private void OnEnable()
        {
            _button.onClick.AddListener(RaiseOnClick);
        }

        public void SetText(string text)
        {
            _text.text = text;
        }

        private void RaiseOnClick()
        {
            OnClick?.Invoke(this);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(RaiseOnClick);
        }
    }
}
