using System;
using DG.Tweening;
using Game;
using R3;
using TMPro;
using Zenject;

namespace UI
{
	public sealed class TargetCellTextPresenter : IInitializable, IDisposable
	{
		private readonly CellChooser _cellChooser;
		private ActiveBundleService _activeBundleService;
		private readonly TMP_Text _tmpText;

		private readonly CompositeDisposable _disposable = new();

		public TargetCellTextPresenter(TMP_Text tmpText, ActiveBundleService activeBundleService, CellChooser cellChooser)
		{
			_tmpText = tmpText;
			_activeBundleService = activeBundleService;
			_cellChooser = cellChooser;
		}

		public void Initialize()
		{
			_cellChooser.DesiredCell
			            .Subscribe(OnCellChanged)
			            .AddTo(_disposable);

			_activeBundleService.OnLevelEnded
			                    .Subscribe(OnLevelEnded)
			                    .AddTo(_disposable);

			FadeInText();
		}

		private void FadeInText()
		{
			var color = _tmpText.color;
			color.a = 0;
			_tmpText.color = color;
			_tmpText.DOFade(1, 3);
		}

		private void OnCellChanged(Cell cell)
		{
			_tmpText.text = $"Find {cell.ID}";
		}

		private void OnLevelEnded(Unit _)
		{
			_tmpText.text = string.Empty;
		}

		public void Dispose()
		{
			_disposable.Clear();
		}
	}
}