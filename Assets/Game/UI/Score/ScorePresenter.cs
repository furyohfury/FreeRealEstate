using System;
using Game.Scoring;
using R3;
using VContainer.Unity;

namespace Game.UI
{
	public sealed class ScorePresenter : IInitializable, IDisposable
	{
		private readonly TextView _scoreTextView;
		private readonly ScoreSystem _scoreSystem;
		private IDisposable _disposable;

		public ScorePresenter(TextView scoreTextView, ScoreSystem scoreSystem)
		{
			_scoreTextView = scoreTextView;
			_scoreSystem = scoreSystem;
		}

		public void Initialize()
		{
			_disposable = _scoreSystem.Score
			                          .Subscribe(OnScoreChanged);
		}

		private void OnScoreChanged(int score)
		{
			_scoreTextView.SetText(score.ToString());
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}