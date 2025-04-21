using System.Collections.Generic;
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
		private UniqueCellsMemorizer _cellMemorizer;
		[ShowInInspector]
		private ActiveBundleService _activeBundleService;

		[ShowIf("HasActiveCell")]
		private CellBundle ActiveCellBundle => _activeBundleService.ActiveCellBundle.CurrentValue;

		[SerializeField]
		private CellViewList _cellViewList;
		[SerializeField] 
		private CellView[] _cellViews;

		[Inject]
		public void Construct(CellChooser cellChooser, UniqueCellsMemorizer uniqueCellsMemorizer, ActiveBundleService activeBundleService)
		{
			_cellChooser = cellChooser;
			_cellMemorizer = uniqueCellsMemorizer;
			_activeBundleService = activeBundleService;
		}

		[Button]
		public void ChooseCell(string cellId)
		{
			var cell = ActiveCellBundle.Cells.Single(cell => cell.ID == cellId);
			_cellChooser.ChooseCell(cell);
		}

		[Button]
		public void SetCellViewList(int rows, int columns)
		{
			_cellViewList.SetCellViews(_cellViews, rows, columns);
		}

		[Button]
		public void DestroyCellViews() => _cellViewList.DestroyViews(); 

		private bool HasActiveCell => _activeBundleService != null;
	}
}