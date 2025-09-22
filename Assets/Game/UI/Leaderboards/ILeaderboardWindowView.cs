namespace Game.UI.Leaderboards
{
	public interface ILeaderboardWindowView : IWindow, IWindowClosable
	{
		void SetBeatmapTitleText(string sourceText);
		void AddRecord(ILeaderboardRecordView leaderboardRecordView);
		void AddPlayerScoreRecord(ILeaderboardRecordView leaderboardRecordView);
	}
}