using DG.Tweening;
using Game;
using UnityEngine.UI;

namespace UI
{
	public sealed class LevelRestarter
	{
		private readonly ActiveBundleService _activeBundleService;
		private readonly Image _loadingScreen;
		private readonly CellViewList _cellListView;
		private BundleSwitcher _bundleSwitcher;

		public LevelRestarter(ActiveBundleService activeBundleService, CellViewList cellListView, Image loadingScreen, BundleSwitcher bundleSwitcher)
		{
			_activeBundleService = activeBundleService;
			_cellListView = cellListView;
			_loadingScreen = loadingScreen;
			_bundleSwitcher = bundleSwitcher;
		}

		public void RestartLevel()
		{
			_loadingScreen.gameObject.SetActive(true);
			var screenColor = _loadingScreen.color;
			screenColor.a = 0;
			_loadingScreen.color = screenColor;
			DOTween.Sequence()
			       .Append(_loadingScreen.DOFade(1, 1))
			       .AppendCallback(() => _activeBundleService.Reset())
			       .AppendCallback(() => _bundleSwitcher.UpdateBundle())
			       .Append(_loadingScreen.DOFade(0, 1))
			       .AppendCallback(() => _loadingScreen.gameObject.SetActive(false));

			_cellListView.EnableViews();
		}
	}
}