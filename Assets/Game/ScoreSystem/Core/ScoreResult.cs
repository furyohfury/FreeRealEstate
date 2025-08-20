using System;
using Game.ElementHandle;

namespace Game.Scoring
{
	[Serializable]
	public readonly struct ScoreResult
	{
		public readonly int Points;
		public readonly JudgementType Judgement;

		public ScoreResult(int points, JudgementType judgement)
		{
			Points = points;
			Judgement = judgement;
		}
	}
}