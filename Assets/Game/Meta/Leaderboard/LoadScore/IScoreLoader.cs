using Cysharp.Threading.Tasks;

namespace Game.Leaderboard
{
	public interface IScoreLoader
	{
		public UniTask<IScoreGetResult> GetRecordsForMap(string mapId, string variantId);
		UniTask<IScoreGetResult> GetMapRecordForUser(string mapId, string variantId, string user);
	}
}