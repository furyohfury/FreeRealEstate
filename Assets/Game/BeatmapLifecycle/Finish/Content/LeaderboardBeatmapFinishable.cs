using Game.UI;
using Game.UI.Leaderboards;
using Game.UI.Loading;

namespace Game.BeatmapFinish
{
	public sealed class LeaderboardBeatmapFinishable : IBeatmapFinishable
	{
		private readonly LeaderboardFactory _leaderboardFactory;
		private readonly IWindowSystem _windowSystem;
		private const int LOADING_SCREEN_PRIORITY = 0;
		private const int LEADERBOARD_WINDOW_PRIORITY = 1;

		public LeaderboardBeatmapFinishable(LeaderboardFactory leaderboardFactory, IWindowSystem windowSystem)
		{
			_leaderboardFactory = leaderboardFactory;
			_windowSystem = windowSystem;
		}

		public async void Finish()
		{
			var dimLoadingScreen = await _windowSystem.Spawn<IDimLoadingScreen>(LOADING_SCREEN_PRIORITY);
			await _leaderboardFactory.Create(LEADERBOARD_WINDOW_PRIORITY);
			_windowSystem.Close(dimLoadingScreen);
		}
	}
}