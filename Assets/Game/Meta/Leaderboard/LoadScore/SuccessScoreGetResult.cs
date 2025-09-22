using System.Collections.Generic;

namespace Game.Leaderboard
{
	public class SuccessScoreGetResult : IScoreGetResult
	{
		public readonly List<LeaderboardRecord> Data;

		public SuccessScoreGetResult(List<LeaderboardRecord> data)
		{
			Data = data;
		}
	}
}