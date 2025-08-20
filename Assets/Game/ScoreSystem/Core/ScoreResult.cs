using System;
using Game.ElementHandle;

namespace Game.Scoring
{
	[Serializable]
	public struct ScoreResult
	{
		public int Points;
		public JudgementType Judgement;

		public ScoreResult(int points, JudgementType judgement)
		{
			Points = points;
			Judgement = judgement;
		}
	}
}