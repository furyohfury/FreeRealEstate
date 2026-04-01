using Game.Application.Ads;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public sealed class GameOverUI : MonoBehaviour
    {
        [SerializeField]
        private Button _continueButton;
        [SerializeField]
        private Button _retryButton;
        [SerializeField]
        private Button _exitButton;

        private void OnEnable()
        {
            _continueButton.onClick.AddListener(OnContinueClicked);
            _retryButton.onClick.AddListener(OnRetryClicked);
            _exitButton.onClick.AddListener(OnExitClicked);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private async void OnContinueClicked()
        {
            await AdsManager.Instance.ShowRewardAd(AdsStaticData.CONTINUE_AD_ID);
            Debug.Log("Continue Ads over");
            Health.Instance.CurrentHealth = Health.Instance.MaxHealth * GameParamsService.Instance.SessionParams.ContinueInitialHealthRatio;
            ItemSystem.Instance.ClearAll();
            GameCycleStateSwitcher.Instance.ResumeGame();
            Hide();
        }

        private void OnRetryClicked()
        {
            SessionRestarter.Instance.Restart();
        }

        private void OnExitClicked()
        {
            SceneManager.LoadScene((int)Scene.MainMenu, LoadSceneMode.Single);
        }

        private void OnDisable()
        {
            _continueButton.onClick.RemoveListener(OnContinueClicked);
            _retryButton.onClick.RemoveListener(OnRetryClicked);
            _exitButton.onClick.RemoveListener(OnExitClicked);
        }
    }
}
