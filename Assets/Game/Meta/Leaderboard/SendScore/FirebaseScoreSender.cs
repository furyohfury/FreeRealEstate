using Cysharp.Threading.Tasks;
using FirebaseSystem;

namespace Game.Leaderboard
{
	public sealed class FirebaseScoreSender : IScoreSender
	{
		private readonly FirebaseManager _firebaseManager;

		public FirebaseScoreSender(FirebaseManager firebaseManager)
		{
			_firebaseManager = firebaseManager;
		}

		public async UniTask<IScoreSendResult> Send(ScoreData data)
		{
			var combinedBeatmapId = FirebaseLeaderboardUtils.GetCollectionCombinedName(data.BeatmapBundleId, data.BeatmapVariantId);
			var payload = new FirebaseScoreData
			              {
				              PlayerDisplayName = data.PlayerDisplayName
				              , PlayerId = data.PlayerId, Score = data.Score
			              };
			ISendDataResult result = await _firebaseManager.SendNestedData(
				FirebaseStaticData.LEADERS_COLLECTION_NAME,
				combinedBeatmapId,
				FirebaseStaticData.LEADERBOARD_SUBCOLLECTION_RESULTS_NAME,
				data.PlayerId,
				payload
				);

			if (result is SuccessSendDataResult)
			{
				return new SuccessScoreSendResult();
			}

			if (result is FailedFirestoreResult failedResult)
			{
				return new FailScoreSendResult(failedResult.Message);
			}

			return new FailScoreSendResult("Unexpected error");
		}
	}
}