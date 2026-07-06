namespace Game
{
    public sealed class PlayerScoreItemPresenterFactory
    {
        private readonly ScoreSystem _scoreSystem;

        public PlayerScoreItemPresenterFactory(ScoreSystem scoreSystem)
        {
            _scoreSystem = scoreSystem;
        }

        public PlayerScoreItemPresenter Create(PlayerData playerData)
        {
            return new PlayerScoreItemPresenter(_scoreSystem, playerData);
        }
    }
}
