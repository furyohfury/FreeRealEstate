using DG.Tweening;
using R3;
using TMPro;
using UnityEngine;

namespace Game
{
    public class PlayerScoreItem : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _playerName;
        [SerializeField]
        private TextMeshProUGUI _score;
        [SerializeField]
        private RectTransform _rectTransform;
        private PlayerScoreItemPresenter _presenter;
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public void Init(PlayerScoreItemPresenter presenter)
        {
            _presenter = presenter;
            SetPlayerName(_presenter.Nickname);
            _presenter.Score.Subscribe(SetScore).AddTo(_disposables);
        }

        public Vector2 GetPosition()
        {
            return _rectTransform.anchoredPosition;
        }

        private void SetPlayerName(string playerName)
        {
            _playerName.text = playerName;
        }

        private void SetScore(string score)
        {
            _score.text = score;
        }

        public Tween Move(Vector2 targetPos, float duration, Ease ease)
        {
            return _rectTransform.DOAnchorPos(targetPos, duration).SetEase(ease);
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }
    }
}
