using Game;
using UnityEngine.UI;
using DOTween;

namespace UI
{
    public sealed class LevelRestarter
    {
        private ActiveLevelService _activeLevelService;
        private Image _loadingScreen;
        // private CellListView _cellListView; TODO add to ctor

        public LevelRestarter(ActiveLevelService activeLevelService)
        {
            _activeLevelService = activeLevelService;
        }

        public void RestartLevel()
        {
            _loadingScreen.gameObject.SetActive(true);
            var screenColor = _loadingScreen.color;
            screenColor.a = 0;
            _loadingScreen.color = screenColor;
            DOTween.Sequence()
            .Append(_ => _loadingScreen.DOFade(1, 0.5f))
            .AppendCallback(_ => _activeLevelService.Reset())
            .Append(_ => _loadingScreen.DOFade(0, 0.5f));

            // _cellListView.EnableViews(); // TODO uncomment
    }
}