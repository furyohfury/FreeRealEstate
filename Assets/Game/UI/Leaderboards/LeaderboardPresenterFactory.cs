using VContainer;

namespace Game.UI.Leaderboards
{
	public sealed class LeaderboardPresenterFactory
	{
		private readonly IObjectResolver _objectResolver;

		public LeaderboardPresenterFactory(IObjectResolver objectResolver)
		{
			_objectResolver = objectResolver;
		}

		public LeaderboardPresenter Create()
		{
			return _objectResolver.Resolve<LeaderboardPresenter>();
		}
	}
}