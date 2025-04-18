using R3;

namespace Game
{
	public sealed class ActiveBundleService
	{
		public Subject<Unit> OnLevelEnded = new();
		public CellBundle ActiveCellBundle => _cellLists[_activeIndex];

		private readonly CellBundle[] _cellLists;
		private int _activeIndex = 0;

		public ActiveBundleService(CellBundle[] cellLists)
		{
			_cellLists = cellLists;
		}

		public void SetNextLevel()
		{
			if (++_activeIndex >= _cellLists.Length)
			{
				OnLevelEnded.OnNext(Unit.Default);
			}
		}

		public void Reset()
		{
			_activeIndex = 0;
		}
	}
}