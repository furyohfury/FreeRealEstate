using System;
using R3;
using Zenject;

namespace Game
{
	public sealed class BundleSwitcher : IInitializable, IDisposable
	{
		private readonly ActiveBundleService _activeBundleService;
		private readonly CellChooser _cellChooser;
		private readonly DesiredCellController _desiredCellController;

		private readonly CompositeDisposable _disposable = new();

		public BundleSwitcher(CellChooser cellChooser, ActiveBundleService activeBundleService, DesiredCellController desiredCellController)
		{
			_cellChooser = cellChooser;
			_activeBundleService = activeBundleService;
			_desiredCellController = desiredCellController;
		}

		public void Initialize()
		{
			_cellChooser.OnGuessed
			            .Subscribe(OnCellGuessed)
			            .AddTo(_disposable);

			SetDesiredCell();
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
			_activeBundleService.SetNextLevel();
			SetDesiredCell();
		}

		private void SetDesiredCell()
		{
			_desiredCellController.UpdateDesiredCell();
		}

		public void Dispose()
		{
			_disposable.Clear();
		}
	}
}