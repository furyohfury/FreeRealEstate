using DG.Tweening;
using Game.Application;
using Game.Application.Leaderboard;
using UnityEngine;

namespace Game
{
    public sealed class PlayerDeadObserver : MonoBehaviour
    {
        [SerializeField]
        private GameOverUI _gameOverUI;
        [SerializeField]
        private bool _isActive;

        private void Start()
        {
            Health.Instance.OnHealthChanged += InstanceOnOnHealthChanged;
            _isActive = AppConfigurationProvider.Instance.Configuration.GetTrackDeath();
        }

        private void InstanceOnOnHealthChanged(float hp)
        {
            if (!_isActive
                || hp > 0)
                return;

            DOTween.KillAll();
            AudioManager.Instance.PlayGameOverSound();
            GameCycleStateSwitcher.Instance.PauseGame();
            LeaderboardManager.Instance.SendResult(GameLoop.Instance.CurrentTime);
            LeaderboardDisplayer.Instance.ShowLeaderboard();
            _gameOverUI.Show();
        }

        private void OnDestroy()
        {
            Health.Instance.OnHealthChanged -= InstanceOnOnHealthChanged;
        }
    }
}
