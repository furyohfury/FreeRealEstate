using System;
using R3;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Gameplay
{
	public sealed class MatchSystem : IInitializable, IDisposable
	{
		public Observable<Unit> OnMatchStarted => _onMatchStarted;
		public readonly Observable<Player> OnMatchWon;

		private readonly RoundRestarter _roundRestarter;
		private readonly GoalObservable _goalObservable;
		private readonly Score _score;
		private readonly GameFinisher _gameFinisher;
		private readonly MatchSettings _matchSettings;

		private readonly Subject<Unit> _onMatchStarted = new();
		private IDisposable _disposable;

		public MatchSystem(RoundRestarter roundRestarter, GoalObservable goalObservable, MatchSettings matchSettings, GameFinisher gameFinisher
			, Score score)
		{
			_roundRestarter = roundRestarter;
			_goalObservable = goalObservable;
			_matchSettings = matchSettings;
			_gameFinisher = gameFinisher;
			OnMatchWon = _gameFinisher.OnPlayerWon;
			_score = score;
		}

		public void Initialize()
		{
			_onMatchStarted.OnNext(Unit.Default);
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

			Debug.Log($"<color=green>Scored goal for player {player.ToString()}</color>");

			if (score + 1 < _matchSettings.PointsToWin)
			{
				_roundRestarter.RestartByGoalHit(player);
			}
			else
			{
				FinishGameByPlayerWonRpc(player);
			}
		}

		[ClientRpc]
		public void FinishGameByPlayerWonRpc(Player player)
		{
			_gameFinisher.FinishGameByPlayerWon(player);
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}