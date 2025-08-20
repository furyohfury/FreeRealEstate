using R3;

namespace Game.Scoring
{
	public sealed class Combo : ICombo
	{
		public ReadOnlyReactiveProperty<int> Count => _count;
		private readonly ReactiveProperty<int> _count = new();

		public void AddCombo()
		{
			_count.Value++;
		}

		public void Reset()
		{
			_count.Value = 0;
		}
	}
}