using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FirebaseSystem;
using Game.Leaderboard;
using Game.Meta.Authentication;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace GameDebug
{
	public sealed class FirebaseLeaderboardHelper : MonoBehaviour
	{
		private const string TEST_GMAIL_COM = "test@gmail.com";
		private const string PASSWORD = "123456";
		private const string DISPLAY_NAME = "TestUser";
		private const string BEATMAP_BUNDLE_ID = "songName";
		private const string BEATMAP_VARIANT_ID = "variandId";
		[Inject]
		private FirebaseManager _firebaseManager;
		[Inject]
		private LeaderboardDataTransferer _dataTransferer;
		[Inject]
		private IUserData _userData;

		[Button]
		public async UniTask LoginDefault()
		{
			var register = await _firebaseManager.Register(TEST_GMAIL_COM, PASSWORD, DISPLAY_NAME);
			if (register is AuthFailure failure)
			{
				var login = await _firebaseManager.SignIn(TEST_GMAIL_COM, PASSWORD);
			}
		}

		[Button]
		public async UniTask LoginSpecific(string mail, string password, string nick)
		{
			var register = await _firebaseManager.Register(mail, password, nick);
			if (register is AuthFailure failure)
			{
				var login = await _firebaseManager.SignIn(mail, password);
			}
		}

		[Button]
		public async UniTask SendData()
		{
			var userData = _userData.GetUserData();
			var playerId = userData.ID;
			var displayName = userData.DisplayName;
			ScoreData scoreData = new ScoreData(BEATMAP_BUNDLE_ID, BEATMAP_VARIANT_ID, playerId, displayName, 1000);
			var result = await _dataTransferer.Send(scoreData);
			if (result is SuccessScoreSendResult scoreSendResult)
			{
				Debug.Log("Successfully sent score");
			}
			else if (result is FailScoreSendResult failResult)
			{
				Debug.LogWarning($"send failure, error - {failResult.Message}");
			}
		}

		[Button]
		public async UniTask GetData()
		{
			var userData = _userData.GetUserData();
			var playerId = userData.ID;
			var displayName = userData.DisplayName;

			IScoreGetResult result = await _dataTransferer.GetMapRecordForUser(BEATMAP_BUNDLE_ID, BEATMAP_VARIANT_ID, playerId);
			if (result is SuccessScoreGetResult successScoreGetResult)
			{
				List<LeaderboardRecord> leaderboardRecords = successScoreGetResult.Data;
				var leaderboardRecord = leaderboardRecords[0];
				Debug.Log($"Successfully got result. Nick: {leaderboardRecord.Nickname}. Score: {leaderboardRecord.Score}");
			}

			if (result is FailScoreGetResult failScoreGetResult)
			{
				Debug.LogWarning($"Couldnt get result. Error: {failScoreGetResult.Message}");
			}

			if (result is NullScoreGetResult)
			{
				Debug.Log("Score doesnt exist");
			}
		}

		[Button]
		public async UniTask GetData(string mapid, string mapVariant, string userId)
		{
			IScoreGetResult result = await _dataTransferer.GetMapRecordForUser(mapid, mapVariant, userId);
			if (result is SuccessScoreGetResult successScoreGetResult)
			{
				List<LeaderboardRecord> leaderboardRecords = successScoreGetResult.Data;
				var leaderboardRecord = leaderboardRecords[0];
				Debug.Log($"Successfully got result. Nick: {leaderboardRecord.Nickname}. Score: {leaderboardRecord.Score}");
			}

			if (result is FailScoreGetResult failScoreGetResult)
			{
				Debug.LogWarning($"Couldnt get result. Error: {failScoreGetResult.Message}");
			}

			if (result is NullScoreGetResult)
			{
				Debug.Log("Score doesnt exist");
			}
		}
	}
}