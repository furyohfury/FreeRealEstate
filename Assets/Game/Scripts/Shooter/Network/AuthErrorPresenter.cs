using Game.Auth;
using UIStackSystem;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Zenject;

namespace Game.UI
{
    public sealed class AuthErrorPresenter : IPresenter, IInitializable
    {
        private readonly AuthorizationSystem _authorizationSystem;
        private readonly AuthErrorUI _authErrorUI;

        public AuthErrorPresenter(AuthorizationSystem authorizationSystem)
        {
            _authorizationSystem = authorizationSystem;
        }

        public void Initialize()
        {
            _authErrorUI.OnRetryPressed += AuthErrorUIOnOnRetryPressed;
            _authErrorUI.OnQuitPressed += AuthErrorUIOnOnQuitPressed;
        }

        private async void AuthErrorUIOnOnRetryPressed()
        {
            await _authorizationSystem.Authorize();

            if (_authorizationSystem.IsAuthorized)
            {
            }
        }

        private void AuthErrorUIOnOnQuitPressed()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void Dispose()
        {
            // _authErrorUI.OnRetryPressed -= AuthErrorUIOnOnRetryPressed;
            // _authErrorUI.OnQuitPressed -= AuthErrorUIOnOnQuitPressed;
        }
    }
}
