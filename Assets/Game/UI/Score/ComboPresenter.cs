using System;
using Game.Scoring;
using R3;
using VContainer.Unity;

namespace Game.UI
{
	public sealed class ComboPresenter : IInitializable, IDisposable
	{
		private readonly TextView _comboTextView;
		private readonly ScoreSystem _scoreSystem;
		private IDisposable _disposable;

		public ComboPresenter(TextView comboTextView, ScoreSystem scoreSystem)
		{
			_comboTextView = comboTextView;
			_scoreSystem = scoreSystem;
		}

		public void Initialize()
		{
			_disposable = _scoreSystem.Combo
			                          .Subscribe(OnComboChanged);
		}

		private void OnComboChanged(int score)
		{
			_comboTextView.SetText(score.ToString());
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}