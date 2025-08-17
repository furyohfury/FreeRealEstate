using System;
using Game.Scoring;
using R3;
using VContainer.Unity;

namespace Game.UI
{
	public sealed class ScorePresenter : IStartable, IDisposable
	{
		private readonly MapScore _mapScore;
		private readonly TextView _scoreTextView;
		private IDisposable _disposable;

		public ScorePresenter(MapScore mapScore, TextView scoreTextView)
		{
			_mapScore = mapScore;
			_scoreTextView = scoreTextView;
		}

		public void Start()
		{
			_disposable = _mapScore.Score
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