using R3;

namespace Game.Scoring
{
	public interface ICombo
	{
		ReadOnlyReactiveProperty<int> Count { get; }
		void AddCombo();
		void Reset();
	}
}