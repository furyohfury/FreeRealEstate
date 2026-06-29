using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using TMPro;
using UIStackSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public sealed class AuthErrorUI : Page<AuthErrorPresenter>
    {
        [SerializeField]
        private Button _retryButton;
        [SerializeField]
        private Button _quitButton;
        [SerializeField]
        private TextMeshProUGUI _errorText;
        
        private AuthErrorPresenter _presenter;
        private readonly CompositeDisposable _disposable =  new CompositeDisposable();

        public override UniTask Open(IPresenter presenter, OpenPageOptions options)
        {
            if (presenter is AuthErrorPresenter authErrorPresenter)
            {
                _presenter = authErrorPresenter;
                _presenter.IsRetryInteractable.Subscribe(ChangeRetryButtonInteractable)
                          .AddTo(_disposable);
                
                _presenter.IsQuitInteractable.Subscribe(ChangeQuitButtonInteractable)
                          .AddTo(_disposable);
                
                _presenter.IsErrorMessageActive.Subscribe(ChangeErrorMessageActive)
                          .AddTo(_disposable);
            }
            else
            {
                Debug.LogError("expected AuthErrorPresenter");
            }
            
            return base.Open(presenter, options);
        }

        private void OnEnable()
        {
            _retryButton.onClick.AddListener(OnRetryPressed);
            _quitButton.onClick.AddListener(OnQuitPressed);
        }

        private void ChangeRetryButtonInteractable(bool interactable)
        {
            _retryButton.interactable = interactable;
        }

        private void ChangeQuitButtonInteractable(bool interactable)
        {
            _quitButton.interactable = interactable;
        }
        
        private void ChangeErrorMessageActive(bool active)
        {
            _errorText.enabled = active;
        }

        private void OnRetryPressed()
        {
            _presenter.AuthErrorUIOnOnRetryPressed();
        }

        private void OnQuitPressed()
        {
            _presenter.AuthErrorUIOnOnQuitPressed();
        }

        private void OnDisable()
        {
            _retryButton.onClick.RemoveListener(OnRetryPressed);
            _quitButton.onClick.RemoveListener(OnQuitPressed);
            _disposable.Dispose();
        }
    }
}
