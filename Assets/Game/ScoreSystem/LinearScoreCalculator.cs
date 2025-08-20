using System.Collections.Generic;
using System.Linq;
using Game.ElementHandle;

namespace Game.Scoring
{
	public sealed class LinearScoreCalculator : IScoreCalculator
	{
		private readonly JudgementSettings _judgementSettings;
		private readonly Dictionary<JudgementType, ScoreResult> _scoreResults;

		public LinearScoreCalculator(JudgementSettings judgementSettings, IEnumerable<ScoreResult> scoreResults)
		{
			_judgementSettings = judgementSettings;
			_scoreResults = scoreResults.ToDictionary(
				result => result.Judgement,
				result => result
			);
		}

		public int JudgeScore(HandleResult handleResult, int combo)
		{
			JudgementResult judgementResult = handleResult.JudgeResult(_judgementSettings);
			ScoreResult scoreResult = _scoreResults[judgementResult.Type];
			var points = scoreResult.Points;
			return points * (1 + combo);
		}
	}
}