using System;
using R3;
using Unity.Netcode;
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
			                             .Subscribe(OnHitGoal);
		}

		private void OnHitGoal(Player player)
		{
			_score.AddPoint(player);
#if UNITY_EDITOR
			UnityEngine.Debug.Log(
				$"<color=green> Goal hit by {player.ToString()}. Score is {_score.GetScore(Player.One)} : {_score.GetScore(Player.Two)}");
#endif
			if (_score.GetScore(player) <= _matchSettings.PointsToWin)
			{
				_roundRestarter.RestartByGoalHit(player);
			}
			else
			{
				FinishGameByPlayerWon(player);
			}
		}

		private void FinishGameByPlayerWon(Player player)
		{
			_gameFinisher.FinishGame(player);
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}