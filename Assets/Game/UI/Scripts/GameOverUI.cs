using DG.Tweening;
using Game.Application.Ads;
using Game.Infrastructure;
using TriInspector;
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
        private RectTransform _rectTransform; 
        [SerializeField]
        private Vector3 _choiceMadeMaxScale = new Vector3(0.2f, 0.2f, 0);
        [SerializeField]
        private float _choiceMadeDuration = 0.5f;
        [SerializeField]
        private Ease _choiceMadeEase = Ease.Linear;
        [SerializeField]
        private float _choiceMadeDecreaseDuration = 0.3f;
        [SerializeField]
        private Ease _choiceMadeDecreaseEase = Ease.Linear;
        private Vector3 _initialScale;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _initialScale = _rectTransform.localScale;
        }

        private void OnEnable()
        {
            _continueButton.onClick.AddListener(OnContinueClicked);
            _retryButton.onClick.AddListener(OnRetryClicked);
            _exitButton.onClick.AddListener(OnExitClicked);
        }

        public void Show()
        {
            _rectTransform.localScale = _initialScale;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private async void OnContinueClicked()
        {
            Sequence disappearSequence = LaunchDisappearSequence();

            await AwaitableExtensions.WaitForTweenRealtime(disappearSequence);

            await AdsManager.Instance.ShowRewardAd(AdsStaticData.CONTINUE_AD_ID);
            Debug.Log("Continue Ads over");

            Health.Instance.CurrentHealth = Health.Instance.MaxHealth * GameParamsService.Instance.SessionParams.ContinueInitialHealthRatio;
            Lane[] lanes = LaneSystem.Instance.Lanes;

            for (int i = 0, count = lanes.Length; i < count; i++)
            {
                ScoreZone scoreZone = lanes[i].ScoreZone;
                scoreZone.StopAllConsumingItems();
            }

            ItemSystem.Instance.ClearAll();
            GameCycleStateSwitcher.Instance.ResumeGame();
            Hide();
        }

        private async void OnRetryClicked()
        {
            Sequence disappearSequence = LaunchDisappearSequence();

            await AwaitableExtensions.WaitForTweenRealtime(disappearSequence);

            SessionRestarter.Instance.Restart();
        }

        private async void OnExitClicked()
        {
            Sequence disappearSequence = LaunchDisappearSequence();

            await AwaitableExtensions.WaitForTweenRealtime(disappearSequence);

            SceneSwitcher.Instance.SwitchScene(Scene.MainMenu, LoadSceneMode.Single);
        }

        [Button]
        private Sequence LaunchDisappearSequence()
        {
            return DOTween.Sequence()
                          .Append(_rectTransform.DOScale(_choiceMadeMaxScale, _choiceMadeDuration).SetEase(_choiceMadeEase))
                          .Append(_rectTransform.DOScale(Vector2.zero, _choiceMadeDecreaseDuration).SetEase(_choiceMadeDecreaseEase))
                          .SetUpdate(true);
        }

        private void OnDisable()
        {
            _continueButton.onClick.RemoveListener(OnContinueClicked);
            _retryButton.onClick.RemoveListener(OnRetryClicked);
            _exitButton.onClick.RemoveListener(OnExitClicked);
        }
    }
}
