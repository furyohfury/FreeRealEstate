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

        private async void InstanceOnOnHealthChanged(float hp)
        {
            if (!_isActive
                || hp > 0)
                return;

            DOTween.KillAll();
            AudioManager.Instance.PlayGameOverSound();
            GameCycleStateSwitcher.Instance.PauseGame();
            await LeaderboardManager.Instance.SendResult(GameLoop.Instance.CurrentTime);
            _gameOverUI.Show();
        }

        private void OnDestroy()
        {
            Health.Instance.OnHealthChanged -= InstanceOnOnHealthChanged;
        }
    }
}
