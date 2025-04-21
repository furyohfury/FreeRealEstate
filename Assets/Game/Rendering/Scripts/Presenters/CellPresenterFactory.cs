namespace Game
{
	public sealed class CellPresenterFactory
	{
		private readonly CellChooser _cellChooser;

		public CellPresenterFactory(CellChooser cellChooser)
		{
			_cellChooser = cellChooser;
		}

		public CellPresenter CreatePresenter(CellView cellView, Cell cell)
		{
			var presenter = new CellPresenter(cellView, _cellChooser, cell);
			return presenter;
		}
	}
}