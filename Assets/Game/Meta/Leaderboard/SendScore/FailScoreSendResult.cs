namespace Game.Leaderboard
{
	public class FailScoreSendResult : IScoreSendResult
	{
		public string Message;

		public FailScoreSendResult(string message)
		{
			Message = message;
		}
	}
}