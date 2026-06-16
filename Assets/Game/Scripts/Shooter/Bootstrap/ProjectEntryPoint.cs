using Game.Auth;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game
{
    public sealed class ProjectEntryPoint : IInitializable
    {
        private readonly AuthorizationSystem _authorizationSystem;

        public ProjectEntryPoint(AuthorizationSystem authorizationSystem)
        {
            _authorizationSystem = authorizationSystem;
        }

        public async void Initialize()
        {
            await _authorizationSystem.Authorize();
            SceneManager.LoadScene(ShooterScenes.MAIN_MENU);
        }
    }
}
