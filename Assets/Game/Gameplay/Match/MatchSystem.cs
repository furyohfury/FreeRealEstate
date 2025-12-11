using System;
using R3;
using Zenject;

namespace Gameplay
{
	public sealed class MatchSystem : IInitializable, IDisposable
	{
		private readonly RoundRestarter _roundRestarter;
		private readonly GoalObservable _goalObservable;
		private readonly Score _score;
		private readonly GameFinisher _gameFinisher;
		private readonly MatchSettings _matchSettings;

		private IDisposable _disposable;

		public MatchSystem(RoundRestarter roundRestarter, GoalObservable goalObservable, MatchSettings matchSettings, GameFinisher gameFinisher
			, Score score)
		{
			_roundRestarter = roundRestarter;
			_goalObservable = goalObservable;
			_matchSettings = matchSettings;
			_gameFinisher = gameFinisher;
			_score = score;
		}

		public void Initialize()
		{
			_disposable = _goalObservable.OnHitGoal
			                             .Subscribe(ScoreGoal);
		}

		public void ScoreGoal(Player player)
		{
			int score = _score.GetScore(player);

			if (score >= _matchSettings.PointsToWin)
			{
				return;
			}
			
			_score.AddPoint(player);
			
			if (score + 1 < _matchSettings.PointsToWin)
			{
				_roundRestarter.RestartByGoalHit(player);
			}
			else
			{
				FinishGameByPlayerWon(player);
			}
		}

		public void FinishGameByPlayerWon(Player player)
		{
			_gameFinisher.FinishGameByPlayerWon(player);
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}
