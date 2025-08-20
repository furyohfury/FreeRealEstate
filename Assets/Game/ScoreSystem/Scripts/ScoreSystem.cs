using Game.ElementHandle;
using R3;

namespace Game.Scoring
{
	public sealed class ScoreSystem
	{
		public ReadOnlyReactiveProperty<int> Combo => _combo.Count;
		public ReadOnlyReactiveProperty<int> Score => _score.Score;

		private readonly ICombo _combo;
		private readonly IMapScore _score;
		private readonly IScoreCalculator _scoreCalculator;

		public ScoreSystem(ICombo combo, IMapScore score, IScoreCalculator scoreCalculator)
		{
			_combo = combo;
			_score = score;
			_scoreCalculator = scoreCalculator;
		}

		public void AddScore(HandleResult result)
		{
			var score = _scoreCalculator.JudgeScore(result, _combo.Count.CurrentValue);
			_score.AddPoints(score);
		}

		public void AddCombo()
		{
			_combo.AddCombo();
		}

		public void ResetCombo()
		{
			_combo.Reset();
		}

		public void ResetScore()
		{
			_score.Reset();
		}
	}
}