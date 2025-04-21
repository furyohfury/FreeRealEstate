namespace Game
{
	public sealed class DesiredCellController
	{
		private readonly ActiveBundleService _activeBundleService;
		private readonly CellChooser _cellChooser;
		private readonly UniqueCellsMemorizer _uniqueCellsMemorizer;

		public DesiredCellController(ActiveBundleService activeBundleService, CellChooser cellChooser, UniqueCellsMemorizer uniqueCellsMemorizer)
		{
			_activeBundleService = activeBundleService;
			_cellChooser = cellChooser;
			_uniqueCellsMemorizer = uniqueCellsMemorizer;
		}

		public void UpdateDesiredCell()
		{
			var activeLevel = _activeBundleService.ActiveCellBundle.CurrentValue;
			var newDesiredCell = _uniqueCellsMemorizer.GetRandomCell(activeLevel);
			_cellChooser.SetDesiredCell(newDesiredCell);
		}
	}
}