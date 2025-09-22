using Cysharp.Threading.Tasks;

namespace Game.Leaderboard
{
	public interface IScoreSender
	{
		UniTask<IScoreSendResult> Send(ScoreData data);
	}
}