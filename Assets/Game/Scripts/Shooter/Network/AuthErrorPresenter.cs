using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Game.Auth;
using R3;
using UIStackSystem;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game.UI
{
    public sealed class AuthErrorPresenter : IPresenter
    {
        public ReactiveProperty<bool> IsRetryInteractable { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> IsQuitInteractable { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> IsErrorMessageActive { get; } = new ReactiveProperty<bool>(true);
        private readonly AuthorizationSystem _authorizationSystem;
        private UIManager _uiManager;

        public AuthErrorPresenter(AuthorizationSystem authorizationSystem, UIManager uiManager)
        {
            _uiManager = uiManager;
            _authorizationSystem = authorizationSystem;
        }

        public async void Init()
        {
            await TryAuthorize();
        }

        public async void AuthErrorUIOnOnRetryPressed()
        {
            await TryAuthorize();
        }

        private async UniTask TryAuthorize()
        {
            IsRetryInteractable.Value = false;
            IsQuitInteractable.Value = false;
            
            await UniTask.WhenAny(UniTask.Delay(5000),
                _authorizationSystem.Authorize());
            
            IsRetryInteractable.Value = true;
            IsQuitInteractable.Value = true;

            if (_authorizationSystem.IsAuthorized)
            {
                IsErrorMessageActive.Value = false;
                Debug.Log($"<color=green>opening next window after auth</color>");
            }
            else
            {
                IsErrorMessageActive.Value = true;
            }
        }

        public void AuthErrorUIOnOnQuitPressed()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void Dispose()
        {
        }
    }
}
