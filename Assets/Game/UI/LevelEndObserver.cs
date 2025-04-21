using System;
using DG.Tweening;
using Game;
using R3;
using UnityEngine.UI;
using Zenject;

namespace UI
{
	public sealed class LevelEndObserver : IInitializable, IDisposable
	{
		private readonly ActiveBundleService _activeBundleService;
		private readonly Button _restartButton;
		private readonly LevelRestarter _levelRestarter;
		private readonly Image _shadowingImage;
		private readonly CellViewList _cellListView;

		private readonly CompositeDisposable _disposable = new();

		public LevelEndObserver(
			ActiveBundleService activeBundleService,
			Button restartButton,
			LevelRestarter levelRestarter,
			Image shadowingImage,
			CellViewList cellListView)
		{
			_activeBundleService = activeBundleService;
			_restartButton = restartButton;
			_levelRestarter = levelRestarter;
			_shadowingImage = shadowingImage;
			_cellListView = cellListView;
		}


		public void Initialize()
		{
			_activeBundleService.OnLevelEnded
			                    .Subscribe(_ => OnLevelEnded())
			                    .AddTo(_disposable);
		}

		private void OnLevelEnded()
		{
			_cellListView.DisableCells();

			_restartButton.gameObject.SetActive(true);
			_restartButton.onClick.AddListener(RestartLevel);

			_shadowingImage.gameObject.SetActive(true);
			var screenColor = _shadowingImage.color;
			screenColor.a = 0;
			_shadowingImage.color = screenColor;
			_shadowingImage.DOFade(0.5f, 0.3f);
		}

		private void RestartLevel()
		{
			_restartButton.onClick.RemoveListener(RestartLevel);
			_restartButton.gameObject.SetActive(false);
			_shadowingImage.gameObject.SetActive(false);
			_levelRestarter.RestartLevel();
		}

		public void Dispose()
		{
			_disposable.Clear();
		}
	}
}