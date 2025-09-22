namespace Game.Leaderboard
{
	public class FailScoreGetResult : IScoreGetResult
	{
		public readonly string Message;

		public FailScoreGetResult(string message)
		{
			Message = message;
		}
	}
}