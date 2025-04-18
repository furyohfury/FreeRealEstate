using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game
{
	public sealed class UniqueCellsMemorizer : IInitializable
	{
		[ShowInInspector]
		private CellBundle[] _cells;
		private readonly Dictionary<CellBundle, bool[]> _chosenCells = new();

		public UniqueCellsMemorizer(CellBundle[] cells)
		{
			_cells = cells;
		}

		public void Initialize()
		{
			InitChosenCells();
		}

		private void InitChosenCells()
		{
			for (int i = 0; i < _cells.Length; i++)
			{
				_chosenCells.Add(_cells[i], new bool[_cells[i].Cells.Length]);
			}
		}

		public Cell GetRandomCell(CellBundle cellBundle)
		{
			var chosenCellsList = _chosenCells[cellBundle];
			if (chosenCellsList.All(chosen => chosen == true))
			{
				ResetChosen(cellBundle);
			}

			int index;
			do
			{
				index = Random.Range(0, chosenCellsList.Length);
			} while (chosenCellsList[index] == true);

			var uniqueCell = cellBundle.Cells[index];
			chosenCellsList[index] = true;
			
			return uniqueCell;
		}

		private void ResetChosen(CellBundle cellBundle)
		{
			Debug.Log("All cells were used. Resetting");
			for (int i = 0; i < _chosenCells[cellBundle].Length; i++)
			{
				bool[] chosenCells = _chosenCells[cellBundle];
				chosenCells[i] = false;
			}
		}
	}
}