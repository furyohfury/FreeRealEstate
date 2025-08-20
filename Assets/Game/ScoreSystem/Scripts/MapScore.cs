using R3;

namespace Game.Scoring
{
	public sealed class MapScore : IMapScore
	{
		public ReadOnlyReactiveProperty<int> Score => _score;
		private readonly ReactiveProperty<int> _score = new();

		public void AddPoints(int points)
		{
			_score.Value += points;
		}

		public void Reset()
		{
			_score.Value = 0;
		}
	}
}