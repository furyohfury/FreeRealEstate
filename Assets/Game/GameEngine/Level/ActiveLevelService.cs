namespace Game{
public sealed class ActiveLevelService
	{
		public Subject<Unit> OnLevelEnded = new();
		public CellList ActiveCellList => _cellLists[_activeIndex];

		private readonly CellList[] _cellLists;
		private int _activeIndex = 0;

        public ActiveLevelService(CellList[] cellLists)
        {
            _cellLists = cellLists;
        }

		public void SetNextLevel()
		{
			if (++_activeIndex >= _cellLists.Length)
			{
				OnLevelEnded.OnNext(Unit.Default);
				return;
			}
		}

		public void Reset()
		{
			_activeIndex = 0;
		}
	}
}