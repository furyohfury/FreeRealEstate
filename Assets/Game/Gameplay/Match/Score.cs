using System.Collections.Generic;

namespace Gameplay
{
	public sealed class Score
	{
		private Dictionary<Player, int> _scores = new Dictionary<Player, int>()
		                                          {
			                                          { Player.One, 0 }, { Player.Two, 0 }
		                                          };
		
		public void AddPoint(Player player)
		{
			_scores[player]++;
		}

		public int GetScore(Player player)
		{
			return _scores[player];
		}

		public void Reset()
		{
			foreach (var player in _scores.Keys)
			{
				_scores[player] = 0;
			}
			{
				
			}
		}
	}
}