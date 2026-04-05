using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public sealed class AnimatedButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private float _maxScale = 1.15f;
        [SerializeField]
        private float _animDuration = 1f;
        private Tween _tween;
        private RectTransform _rectTransform;
        private Vector3 _initialScale;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _initialScale = _rectTransform.localScale;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _tween = _rectTransform.DOScale(Vector3.one * _maxScale, _animDuration).SetLoops(-1, LoopType.Yoyo);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _tween?.Kill();
            _rectTransform.localScale = _initialScale;
        }
    }
}
