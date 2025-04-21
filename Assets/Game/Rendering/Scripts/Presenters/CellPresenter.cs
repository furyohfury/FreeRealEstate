using R3;

namespace Game
{
	public sealed class CellPresenter
	{
		private readonly Cell _cell;
		private readonly CellView _cellView;
		private readonly CellChooser _cellChooser;

		private readonly CompositeDisposable _disposable = new();

		public CellPresenter(CellView cellView, CellChooser cellChooser, Cell cell)
		{
			_cellView = cellView;
			_cellChooser = cellChooser;
			_cell = cell;
			
			_cellView.gameObject.name = $"{_cell.ID} cell";
			_cellView.OnClicked
			         .Subscribe(OnViewClicked)
			         .AddTo(_disposable);
		}

		private void OnViewClicked(Unit _)
		{
			bool guess = _cellChooser.ChooseCell(_cell);

			if (guess == false)
			{
				_cellView.DoWrongChoiceEffect();
			}
			else
			{
				_cellView.DoCorrectChoiceEffect();
			}
		}

		public void Dispose()
		{
			_disposable.Clear();
		}
	}
}