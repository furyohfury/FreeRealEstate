using R3;

namespace Game.Scoring
{
	public interface IMapScore
	{
		ReadOnlyReactiveProperty<int> Score { get; }
		void AddPoints(int points);
		void Reset();
	}
}