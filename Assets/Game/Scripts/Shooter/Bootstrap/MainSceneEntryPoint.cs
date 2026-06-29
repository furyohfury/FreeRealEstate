using Game.Auth;
using Game.UI;
using UIStackSystem;
using Zenject;

namespace Game
{
    public sealed class MainSceneEntryPoint : IInitializable
    {
        private readonly AuthorizationSystem _authorizationSystem;
        private readonly UIManager _uiManager;

        public MainSceneEntryPoint(AuthorizationSystem authorizationSystem, UIManager uiManager)
        {
            _authorizationSystem = authorizationSystem;
            _uiManager = uiManager;
        }
        
        public async void Initialize()
        {
            if (_authorizationSystem.IsAuthorized == false)
            {
                await _uiManager.OpenPage<AuthErrorPresenter>();
            }
        }
    }
}
