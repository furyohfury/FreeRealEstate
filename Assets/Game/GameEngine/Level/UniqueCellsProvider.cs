using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game
{
	public sealed class UniqueCellsProvider : IInitializable // TODO rename to UniqueCellsMemorizer
	{
		[ShowInInspector]
		private CellList[] _cells;
		private Dictionary<CellList, bool[]> _chosenCells = new();

		public UniqueCellsProvider(CellList[] cells)
		{
			_cells = cells;
		}

		public void Initialize()
		{
			InitChosenCells();
		}

		public void InitChosenCells()
		{
			for (int i = 0; i < cells.Length; i++)
			{
				_chosenCells.Add(cells[i], new bool[cells[i].Length]);
			}
		}

		public Cell GetRandomCell(CellList cellList)
		{
			var chosenCellsList = _chosenCells[cellList];
			if (chosenCellsList.All(chosen => chosen == true))
			{
				ResetChosen(CellList cellList);
			}

			int index;
			do 
			{
				index = Random.Range(0, chosenCellsList.Length - 1);
			} 
			while (chosenCellsList[index] == true);

			return cellList[index];
		}

		private void ResetChosen(CellList cellList)
		{
			Debug.Log("All cells were used. Resetting");
			for (int i = 0; i < _chosenCells[cellList].Length; i++)
			{
				_chosenCells[i] = false;
			}
		}
	}
}