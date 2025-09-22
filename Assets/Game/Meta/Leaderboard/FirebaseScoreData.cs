using Firebase.Firestore;

namespace Game.Leaderboard
{
	[FirestoreData]
	public struct FirebaseScoreData
	{
		[FirestoreProperty]
		public string PlayerId { get; set; }

		[FirestoreProperty]
		public string PlayerDisplayName { get; set; }

		[FirestoreProperty]
		public double Score { get; set; }
	}
}