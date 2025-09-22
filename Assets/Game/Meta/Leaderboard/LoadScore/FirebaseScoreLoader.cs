using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FirebaseSystem;

namespace Game.Leaderboard
{
	public class FirebaseScoreLoader : IScoreLoader
	{
		private readonly FirebaseManager _firebaseManager;

		private const string SCORE_PATH = "Score";

		public FirebaseScoreLoader(FirebaseManager firebaseManager)
		{
			_firebaseManager = firebaseManager;
		}

		public async UniTask<IScoreGetResult> GetRecordsForMap(string mapId, string variantId)
		{
			var collectionCombinedName = FirebaseLeaderboardUtils.GetCollectionCombinedName(mapId, variantId);
			IGetDataResult result = await _firebaseManager.GetDocumentsByQuery<FirebaseScoreData>(
				FirebaseStaticData.LEADERS_COLLECTION_NAME,
				collectionCombinedName,
				FirebaseStaticData.LEADERBOARD_SUBCOLLECTION_RESULTS_NAME,
				query => query.OrderByDescending(SCORE_PATH)
				);
			if (result is SuccessGetDataResult<FirebaseScoreData> success)
			{
				var databaseDocuments = success.Data;
				var records = WriteRecordsToList(databaseDocuments);
				return new SuccessScoreGetResult(records);
			}

			if (result is FailedFirestoreResult failedResult)
			{
				return new FailScoreGetResult(failedResult.Message);
			}

			if (result is NullFirestoreResult)
			{
				return new NullScoreGetResult();
			}

			throw new Exception("Returned unexpected result type");
		}

		public async UniTask<IScoreGetResult> GetMapRecordForUser(string mapId, string variantId, string user)
		{
			var collectionCombinedName = FirebaseLeaderboardUtils.GetCollectionCombinedName(mapId, variantId);
			IGetDataResult result = await _firebaseManager.GetDocumentsByQuery<FirebaseScoreData>(
				FirebaseStaticData.LEADERS_COLLECTION_NAME,
				collectionCombinedName,
				FirebaseStaticData.LEADERBOARD_SUBCOLLECTION_RESULTS_NAME,
				query => query.WhereEqualTo("PlayerId", user)
				);
			if (result is SuccessGetDataResult<FirebaseScoreData> success)
			{
				if (success.Data.Count <= 0)
				{
					return new NullScoreGetResult();
				}

				if (success.Data.Count >= 2)
				{
					return new FailScoreGetResult("User had more than 2 scores");
				}

				var databaseDocuments = success.Data;
				var records = WriteRecordsToList(databaseDocuments);
				return new SuccessScoreGetResult(records);
			}

			if (result is FailedFirestoreResult failedResult)
			{
				return new FailScoreGetResult(failedResult.Message);
			}

			if (result is NullFirestoreResult)
			{
				return new NullScoreGetResult();
			}

			throw new Exception("Returned unexpected result type");
		}

		private static List<LeaderboardRecord> WriteRecordsToList(List<FirebaseScoreData> documents)
		{
			var records = new List<LeaderboardRecord>();
			foreach (var document in documents)
			{
				records.Add(new LeaderboardRecord()
				            {
					            Nickname = document.PlayerDisplayName, Score = document.Score
				            });
			}

			return records;
		}
	}
}