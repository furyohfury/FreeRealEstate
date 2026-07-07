using System;
using R3;

namespace Game
{
    public sealed class PlayerScoreItemPresenter
    {
        public ReactiveProperty<string> Score = new ReactiveProperty<string>("0");
        public readonly string Nickname;

        private readonly IDisposable _disposable;
        private readonly PlayerData _playerData;

        public PlayerScoreItemPresenter(ScoreSystem scoreSystem, PlayerData playerData)
        {
            _playerData = playerData;
            Nickname = playerData.Nickname.ToString();
            _disposable = scoreSystem.OnScoreChanged.Subscribe(OnScoreChanged);
        }

        private void OnScoreChanged(PlayerScoreData scoreData)
        {
            if (_playerData != scoreData.PlayerData)
                return;

            Score.Value = scoreData.Score.ToString();
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
