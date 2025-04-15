using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game
{
	public class DebugHelper : MonoBehaviour
	{
		[ShowInInspector]
		private CellChooser _cellChooser;
		[ShowInInspector]
		private UniqueCellsProvider _cellProvider;
		[SerializeField]
		private CellList _cellList;

		[Inject]
		public void Construct(CellChooser cellChooser, UniqueCellsProvider uniqueCellsProvider)
		{
			_cellChooser = cellChooser;
			_cellProvider = uniqueCellsProvider;
		}

		[Button]
		public void ChooseCell(string cellId)
		{
			var cell = _cellList.Cells.Single(cell => cell.ID == cellId);
			_cellChooser.ChooseCell(cell);
		}
	}
}