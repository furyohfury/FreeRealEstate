namespace Game.Leaderboard
{
	public static class FirebaseLeaderboardUtils
	{
		public static string GetCollectionCombinedName(string bundleId, string variantId)
		{
			return $"{bundleId}-{variantId}";
		}
	}
}