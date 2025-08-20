using Game.ElementHandle;

namespace Game.Scoring
{
	public interface IScoreCalculator
	{
		int JudgeScore(HandleResult result, int combo);
	}
}