using Cysharp.Threading.Tasks;
using Game.BeatmapLaunch;
using Game.Leaderboard;
using Game.Meta.Authentication;
using Game.Scoring;
using UnityEngine;

namespace Game.UI.Leaderboards
{
	public sealed class LeaderboardPresenter
	{
		private readonly IWindowSystem _windowSystem;
		private readonly LeaderboardDataTransferer _dataTransferer;
		private readonly CurrentBundleService _currentBundleService;
		private readonly IUserData _userData;
		private readonly ScoreSystem _scoreSystem;

		private int _leaderboardRecordPriority;
		private int _messagePriority;
		private const int MAX_LEADERBOARD_VIEWS = 50;

		public LeaderboardPresenter(
			LeaderboardDataTransferer dataTransferer,
			CurrentBundleService currentBundleService,
			IUserData userData,
			ScoreSystem scoreSystem,
			IWindowSystem windowSystem
			)
		{
			_dataTransferer = dataTransferer;
			_currentBundleService = currentBundleService;
			_userData = userData;
			_scoreSystem = scoreSystem;
			_windowSystem = windowSystem;
		}

		public async UniTask Init(ILeaderboardWindowView leaderboardWindowView, int leaderboardPriority)
		{
			UserData userData = _userData.GetUserData();
			var score = _scoreSystem.Score.CurrentValue;
			var beatmapBundleId = _currentBundleService.CurrentBundle.SongId;
			var beatmapVariantId = _currentBundleService.CurrentVariant.Id;

			_leaderboardRecordPriority = leaderboardPriority + 1;
			_messagePriority = _leaderboardRecordPriority + 1;
			leaderboardWindowView.SetBeatmapTitleText($"{beatmapBundleId}-{beatmapVariantId}");
			var playerRecordView = await _windowSystem.Spawn<ILeaderboardRecordView>(_leaderboardRecordPriority);
			playerRecordView.SetNicknameText(userData.DisplayName);
			leaderboardWindowView.AddPlayerScoreRecord(playerRecordView);

			await HandlePlayerScore(beatmapBundleId, beatmapVariantId, userData, score, playerRecordView);

			await HandleLeaderboardRecords(beatmapBundleId, beatmapVariantId, userData, leaderboardWindowView, playerRecordView);
		}

		private async UniTask HandlePlayerScore(
			string beatmapBundleId,
			string beatmapVariantId,
			UserData userData,
			int score,
			ILeaderboardRecordView playerRecordView
			)
		{
			var result = await _dataTransferer.GetMapRecordForUser(beatmapBundleId, beatmapVariantId, userData.ID);
			int maxScore = score;
			if (result is SuccessScoreGetResult success)
			{
				var dbPlayerScore = (int)success.Data[0].Score;
				maxScore = Mathf.Max(maxScore, dbPlayerScore);
				if (score > dbPlayerScore)
				{
					var sendResult = await SendPlayerScore(beatmapBundleId, beatmapVariantId, userData, score);
					if (sendResult is FailScoreSendResult failSendResult)
					{
						await DisplayErrorMessage(failSendResult.Message);
					}
				}
			}
			else
			{
				var sendResult = await SendPlayerScore(beatmapBundleId, beatmapVariantId, userData, score);
				if (sendResult is FailScoreSendResult failSendResult)
				{
					await DisplayErrorMessage(failSendResult.Message);
				}
			}

			playerRecordView.SetScoreText(maxScore.ToString());
		}

		private async UniTask HandleLeaderboardRecords(
			string beatmapBundleId,
			string beatmapVariantId,
			UserData userData,
			ILeaderboardWindowView leaderboardWindowView,
			ILeaderboardRecordView playerRecordView
			)
		{
			var result = await _dataTransferer.GetRecordsForMap(beatmapBundleId, beatmapVariantId);
			if (result is SuccessScoreGetResult success)
			{
				var records = success.Data;
				for (int i = 0, count = records.Count; i < count; i++)
				{
					if (records[i].Nickname == userData.DisplayName)
					{
						playerRecordView.SetPositionText((i + 1).ToString());
					}
				}

				for (int i = 0, count = Mathf.Min(MAX_LEADERBOARD_VIEWS, records.Count); i < count; i++)
				{
					var recordView = await _windowSystem.Spawn<ILeaderboardRecordView>(_leaderboardRecordPriority);
					recordView.SetPositionText((i + 1).ToString());
					recordView.SetNicknameText(records[i].Nickname);
					recordView.SetScoreText(records[i].Score.ToString("N0"));
					leaderboardWindowView.AddRecord(recordView);
				}
			}
			else if (result is FailScoreGetResult failScoreGetResult)
			{
				DisplayErrorMessage(failScoreGetResult.Message).Forget();
			}
		}

		private async UniTask<IScoreSendResult> SendPlayerScore(
			string beatmapBundleId,
			string beatmapVariantId,
			UserData userData, int score
			)
		{
			var scoreData = new ScoreData(
				beatmapBundleId,
				beatmapVariantId,
				userData.ID, userData.DisplayName,
				score
				);
			return await _dataTransferer.Send(scoreData);
		}

		private async UniTask DisplayErrorMessage(string message)
		{
			var messagePopup = await _windowSystem.Spawn<IMessageWindow>(_messagePriority);
			messagePopup.SetMessageText($"Error: {message}");
		}
	}
}