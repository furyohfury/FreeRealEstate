using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class PlayerScoreItem : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private TextMeshProUGUI _playerName;
        [SerializeField]
        private TextMeshProUGUI _score;
        [SerializeField]
        private Image _avatar;
        [SerializeField]
        private RectTransform _rectTransform;
        
        public Vector2 GetPosition() => _rectTransform.anchoredPosition;

        public void SetPlayerName(string playerName)
        {
            _playerName.text = playerName;
        }

        public void SetScore(string score)
        {
            _score.text = score;
        }

        public void SetAvatar(Sprite avatar)
        {
            _avatar.sprite = avatar;
        }

        public Tween Move(Vector2 targetPos, float duration, Ease ease)
        {
            return _rectTransform.DOAnchorPos(targetPos, duration).SetEase(ease);
        }
    }
}
