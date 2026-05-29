using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public sealed class AuthErrorUI : MonoBehaviour
    {
        public event Action OnRetryPressed;
        public event Action OnQuitPressed;
        
        [SerializeField]
        private Button _retryButton;
        [SerializeField]
        private Button _quitButton;
        [SerializeField]
        private TextMeshProUGUI _errorText;

        private void OnEnable()
        {
            _retryButton.onClick.AddListener(OnRetry);
            _quitButton.onClick.AddListener(OnQuit);
        }

        public void SetErrorTextActive(bool active)
        {
            _errorText.gameObject.SetActive(active);
        }

        public void SetRetryButtonInteractable(bool interactable)
        {
            _retryButton.interactable = interactable;
        }

        private void OnRetry()
        {
            OnRetryPressed?.Invoke();
        }

        private void OnQuit()
        {
            OnQuitPressed?.Invoke();
        }

        private void OnDisable()
        {
            _retryButton.onClick.RemoveListener(OnRetry);
            _quitButton.onClick.RemoveListener(OnQuit);
        }
    }
}
