using Cysharp.Threading.Tasks;

namespace Game.UI.Leaderboards
{
	public sealed class LeaderboardFactory
	{
		private readonly LeaderboardPresenterFactory _leaderboardPresenterFactory;
		private readonly IWindowSystem _windowSystem;

		public LeaderboardFactory(LeaderboardPresenterFactory leaderboardPresenterFactory, IWindowSystem windowSystem)
		{
			_leaderboardPresenterFactory = leaderboardPresenterFactory;
			_windowSystem = windowSystem;
		}

		public async UniTask Create(int priority)
		{
			var view = await _windowSystem.Spawn<ILeaderboardWindowView>(priority);
			var presenter = _leaderboardPresenterFactory.Create();
			await presenter.Init(view, priority);
		}
	}
}