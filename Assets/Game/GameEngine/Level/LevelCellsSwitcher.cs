using System;
using R3;
using Zenject;

namespace Game
{
	public sealed class LevelCellsSwitcher : IInitializable, IDisposable
	{		
		private ActiveLevelService _activeLevelService;
		private readonly CellChooser _cellChooser;		

		private readonly CompositeDisposable _disposable = new();

		public LevelCellsSwitcher(CellChooser cellChooser, ActiveLevelService activeLevelService)
		{
			_cellChooser = cellChooser;
			_activeLevelService = activeLevelService;
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

			SetActiveList();
		}

		private void SetActiveList()
		{
			_activeLevelService.SetNextLevel();
		}		

		public void Dispose()
		{
			_disposable.Clear();
		}
	}
}