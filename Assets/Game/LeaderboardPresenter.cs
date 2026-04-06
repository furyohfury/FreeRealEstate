using Game.Infrastructure;
using UnityEngine;

namespace Game
{
    public abstract class LeaderboardPresenter : MonoBehaviour
    {
        [SerializeField]
        private ButtonUI _restartButton;
        [SerializeField]
        private ButtonUI _quitButton;

        private void OnEnable()
        {
            _restartButton.OnClick += RestartButtonOnOnClick;
            _quitButton.OnClick += QuitButtonOnOnClick;
        }

        private void RestartButtonOnOnClick(ButtonUI obj)
        {
            SessionRestarter.Instance.Restart();
        }

        private void QuitButtonOnOnClick(ButtonUI obj)
        {
            SceneSwitcher.Instance.SwitchSceneWithLoadingScreen(Scene.MainMenu);
        }

        private void OnDisable()
        {
            _restartButton.OnClick -= RestartButtonOnOnClick;
            _quitButton.OnClick -= QuitButtonOnOnClick;
        }

        public abstract void ShowLeaderboard();
        public abstract void HideLeaderboard();
    }
}
