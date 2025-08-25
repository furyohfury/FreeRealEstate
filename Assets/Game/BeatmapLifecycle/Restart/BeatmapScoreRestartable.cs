using Game.Scoring;

namespace Game.BeatmapRestart
{
	public sealed class BeatmapScoreRestartable : IBeatmapRestartable
	{
		private readonly ScoreSystem _scoreSystem;

		public BeatmapScoreRestartable(ScoreSystem scoreSystem)
		{
			_scoreSystem = scoreSystem;
		}

		public void Restart()
		{
			_scoreSystem.ResetScore();
			_scoreSystem.ResetCombo();
		}
	}
}