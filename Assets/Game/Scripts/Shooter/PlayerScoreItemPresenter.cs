using System;
using R3;

namespace Game
{
    public sealed class PlayerScoreItemPresenter
    {
        public ReactiveProperty<string> Place = new ReactiveProperty<string>("0");
        public ReactiveProperty<string> Score = new ReactiveProperty<string>("0");
        public readonly string Nickname;

        private readonly ScoreSystem _scoreSystem;
        private readonly IDisposable _disposable;
        private readonly PlayerData _playerData;

        public PlayerScoreItemPresenter(ScoreSystem scoreSystem, PlayerData playerData)
        {
            _playerData = playerData;
            _scoreSystem = scoreSystem;
            Nickname = playerData.Nickname.ToString();
            _disposable = _scoreSystem.OnScoreChanged.Subscribe(OnScoreChanged);
        }

        private void OnScoreChanged(PlayerScoreData scoreData)
        {
            if (_playerData != scoreData.PlayerData)
                return;

            Score.Value = scoreData.Score.ToString();
            Place.Value = _scoreSystem.GetPlace(scoreData.PlayerData).ToString();
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
