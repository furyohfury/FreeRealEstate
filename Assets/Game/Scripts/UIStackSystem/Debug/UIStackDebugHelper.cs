using Game.UI;
using TriInspector;
using UnityEngine;
using Zenject;

namespace UIStackSystem.Debug
{
    public class UIStackDebugHelper : MonoBehaviour
    {
        [Inject]
        private UIManager _uiManager;

        [Button]
        public async void OpenAuthorizationPage()
        {
            OpenPageOptions withAnimationMode = OpenPageOptions.Create()
                                                               .WithAnimationMode(UIPageAnimationMode.SlideRight);
            await _uiManager.OpenPage<AuthErrorPresenter>(withAnimationMode);
        }

        [Button]
        public async void CloseTop()
        {
            var options = ClosePageOptions.Create()
                                          .WithAnimationMode(UIPageAnimationMode.SlideLeft);
            await _uiManager.CloseTop(options);
        }
    }
}
