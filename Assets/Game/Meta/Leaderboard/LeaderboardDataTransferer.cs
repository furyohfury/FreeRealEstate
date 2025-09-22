using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.Leaderboard
{
	public sealed class LeaderboardDataTransferer
	{
		private readonly IScoreSender _scoreSender;
		private readonly IScoreLoader _scoreLoader;

		private readonly SemaphoreSlim _semaphore = new(1, 1);

		public LeaderboardDataTransferer(IScoreSender scoreSender, IScoreLoader scoreLoader)
		{
			_scoreSender = scoreSender;
			_scoreLoader = scoreLoader;
		}

		public async UniTask<IScoreSendResult> Send(ScoreData data)
		{
			await _semaphore.WaitAsync();
			var result = await _scoreSender.Send(data);
			_semaphore.Release();
			return result;
		}

		public async UniTask<IScoreGetResult> GetRecordsForMap(string mapId, string variantId)
		{
			await _semaphore.WaitAsync();
			var result = await _scoreLoader.GetRecordsForMap(mapId, variantId);
			_semaphore.Release();
			return result;
		}

		public UniTask<IScoreGetResult> GetMapRecordForUser(string mapId, string variantId, string userId)
		{
			return _scoreLoader.GetMapRecordForUser(mapId, variantId, userId);
		}

		public async UniTask UpdateLeaderboard()
		{
			await _semaphore.WaitAsync();
			// TODO
			_semaphore.Release();
		}
	}
}