using System;
using Gameplay;
using R3;
using Zenject;

namespace Game.UI
{
	public sealed class ScoreUIObserver : IInitializable, IDisposable
	{
		private readonly ITextView _view;
		private readonly Score _score;
		private readonly Player _player;
		private IDisposable _disposable;

		public ScoreUIObserver(ITextView view, Score score, Player player)
		{
			_view = view;
			_score = score;
			_player = player;
		}

		public void Initialize()
		{
			Observable<int> observable = _score.GetScoreObservable(_player);
			_disposable = observable.Subscribe(
				score => _view.SetText(score.ToString())
				);
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}