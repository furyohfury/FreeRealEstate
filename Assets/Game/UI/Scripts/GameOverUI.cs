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
        [Header("References")]
        [SerializeField]
        private GameObject _contentContainer;
        [SerializeField]
        private LeaderboardViewMock _mockLeaderboardPrefab;
        [SerializeField]
        private LeaderboardViewYG _leaderboardViewYgPrefab;
        [SerializeField]
        private Transform _leaderboardContainer;
        [SerializeField]
        private Button _continueButton;
        [SerializeField]
        private Button _retryButton;
        [SerializeField]
        private Button _exitButton;
        [Header("Parameters")]
        [SerializeField]
        private Mode _mode = Mode.Mock;
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
        [SerializeField]
        private int _numberOfAdContinues = 1;

        private LeaderboardView _leaderboardView;
        private RectTransform _rectTransform;
        private Vector3 _initialScale;
        private int _currentRetries = 0;

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
            _contentContainer.SetActive(true);
#if UNITY_EDITOR
            switch (_mode)
            {
                case Mode.Mock:
                    _leaderboardView = Instantiate(_mockLeaderboardPrefab, _leaderboardContainer);
                    break;
                case Mode.YG:
                    _leaderboardView = Instantiate(_leaderboardViewYgPrefab, _leaderboardContainer);
                    break;
            }
#elif UNITY_WEBGL
                    _leaderboardView = Instantiate(_leaderboardViewYgPrefab, _leaderboardContainer);
#endif

            _leaderboardView.UpdateLeaderboard();
            
            if (_rectTransform != null)
            {
                _rectTransform.localScale = _initialScale;
            }

            _continueButton.interactable = _currentRetries < _numberOfAdContinues;
        }

        private async void OnContinueClicked()
        {
            _currentRetries++;
            Sequence disappearSequence = LaunchDisappearSequence();

            await AwaitableExtensions.WaitForTweenRealtime(disappearSequence);

            await AdsManager.Instance.ShowRewardAd(AdsStaticData.CONTINUE_AD_ID);
            Debug.Log("Continue Ads over");

            Health.Instance.CurrentHealth = Health.Instance.MaxHealth * GameParamsService.Instance.SessionParams.ContinueInitialHealthRatio;
            Lane[] lanes = LaneSystem.Instance.Lanes;

            for (int i = 0, count = lanes.Length; i < count; i++)
            {
                lanes[i].Speed *= GameParamsService.Instance.SessionParams.ContinueSpeedRatio;
                ScoreZone scoreZone = lanes[i].ScoreZone;
                scoreZone.StopAllConsumingItems();
            }

            ItemSystem.Instance.ClearAll();
            GameCycleStateSwitcher.Instance.ResumeGame();
            Hide();
        }

        private void Hide()
        {
            Destroy(_leaderboardView.gameObject);
            _contentContainer.SetActive(false);
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

        private enum Mode
        {
            Mock,
            YG
        }
    }
}
