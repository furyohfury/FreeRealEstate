namespace Game.Leaderboard
{
	public struct ScoreData
	{
		public readonly string BeatmapBundleId;
		public readonly string BeatmapVariantId;
		public readonly string PlayerId;
		public readonly string PlayerDisplayName;
		public readonly long Score;

		public ScoreData(string beatmapBundleId, string beatmapVariantId, string playerId, string playerDisplayName, int score)
		{
			BeatmapBundleId = beatmapBundleId;
			BeatmapVariantId = beatmapVariantId;
			PlayerId = playerId;
			PlayerDisplayName = playerDisplayName;
			Score = score;
		}
	}
}