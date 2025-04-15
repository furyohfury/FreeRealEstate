using System;
using R3;
using Zenject;

namespace Game
{
	public sealed class LevelCellsSwitcher : IInitializable, IDisposable
	{
		public Subject<Unit> OnLevelEnded = new();

		private readonly CellList[] _cellLists;
		private readonly CellChooser _cellChooser;
		private readonly UniqueCellsProvider _uniqueCellsProvider;
		private int _activeIndex = 0;

		private readonly CompositeDisposable _disposable = new();

		public LevelCellsSwitcher(CellChooser cellChooser, UniqueCellsProvider uniqueCellsProvider, CellList[] cellLists)
		{
			_cellChooser = cellChooser;
			_uniqueCellsProvider = uniqueCellsProvider;
			_cellLists = cellLists;
		}

		public void Initialize()
		{
			_cellChooser.OnGuessed
			            .Subscribe(OnCellGuessed)
			            .AddTo(_disposable);

			SetActiveList();
		}

		private void OnCellGuessed(bool guess)
		{
			if (guess == false)
			{
				return;
			}

			++_activeIndex;
			if (_activeIndex >= _cellLists.Length)
			{
				OnLevelEnded.OnNext(Unit.Default);
				return;
			}

			SetActiveList();
		}

		private void SetActiveList()
		{
			var newList = _cellLists[_activeIndex];
			_uniqueCellsProvider.SetCells(newList.Cells);
			var randomCell = _uniqueCellsProvider.GetRandomCell();
			_cellChooser.SetDesiredCell(randomCell);
		}

		public void Reset()
		{
			_activeIndex = 0;
			SetActiveList();
		}

		public void Dispose()
		{
			_disposable.Clear();
		}
	}
}