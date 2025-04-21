using R3;

namespace Game
{
	public sealed class ActiveBundleService
	{
		public Subject<Unit> OnLevelEnded = new();
		public ReadOnlyReactiveProperty<CellBundle> ActiveCellBundle => _activeCellBundle;

		private readonly ReactiveProperty<CellBundle> _activeCellBundle = new();

		private readonly CellBundle[] _cellLists;
		private int _activeIndex = 0;

		public ActiveBundleService(CellBundle[] cellLists)
		{
			_cellLists = cellLists;
			_activeCellBundle.Value = _cellLists[_activeIndex];
		}

		public void SetNextLevel()
		{
			if (++_activeIndex >= _cellLists.Length)
			{
				OnLevelEnded.OnNext(Unit.Default);
			}

			_activeCellBundle.Value = _cellLists[_activeIndex];
		}

		public void Reset()
		{
			_activeIndex = 0;
		}
	}
}