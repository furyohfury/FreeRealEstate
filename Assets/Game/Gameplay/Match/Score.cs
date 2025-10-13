using System.Collections.Generic;
using R3;

namespace Gameplay
{
	public sealed class Score
	{
		private readonly Dictionary<Player, ReactiveProperty<int>> _scores;

		public Score()
		{
			var playerOneScore = new ReactiveProperty<int>();
			var playerTwoScore = new ReactiveProperty<int>();
			_scores = new Dictionary<Player, ReactiveProperty<int>>()
			          {
				          { Player.One, playerOneScore }, { Player.Two, playerTwoScore }
			          };
		}

		public Observable<int> GetScoreObservable(Player player)
		{
			return _scores[player];
		}

		public void AddPoint(Player player)
		{
			_scores[player].Value++;
		}

		public int GetScore(Player player)
		{
			return _scores[player].CurrentValue;
		}

		public void Reset()
		{
			foreach (var player in _scores.Keys)
			{
				_scores[player].Value = 0;
			}
		}
	}
}