using UnityEngine.UI;
using Zenject;
using R3;
using Game;

namespace UI
{
    public sealed class LevelEndObserver : IInitializable, IDisposable
    {
        private ActiveLevelService _activeLevelService;
        private Button _restartButton;
        private LevelRestarter _levelRestarter;
        private Image _shadowingImage;
        // private CellListView _cellListView; TODO add to ctor

        private CompositeDisposable _disposable = new();

        public LevelEndObserver(
            ActiveLevelService activeLevelService, 
            Button restartButton, 
            LevelRestarter levelRestarter,
            Image _shadowingImage)
            {
                _activeLevelService = activeLevelService;
                _restartButton = restartButton;
                _levelRestarter = levelRestarter;
                _shadowingImage = shadowingImage;
            }


        public void Initialize()
        {
            _activeLevelService.OnLevelEnded
            .Subscribe(OnLevelEnded)
            .AddTo(_disposable);
        }

        private void OnLevelEnded()
        {
            // _cellListView.DisableCells(); // TODO uncomment

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
            _levelRestarter.RestartLevel();
        }

        public void Dispose()
        {
            _disposable.Clear();
        }

    }    
}