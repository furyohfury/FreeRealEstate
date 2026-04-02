using System.Collections.Generic;
using DG.Tweening;
using Game.Utils;
using TriInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public sealed class DifficultySelectionUI : MonoBehaviour
    {
        [SerializeField]
        private SessionParamsStorage _sessionParamsStorage;
        [SerializeField]
        private Transform _container;
        [SerializeField]
        private ButtonUI _buttonPrefab;
        private readonly Dictionary<ButtonUI, int> _buttonToParamsMap = new Dictionary<ButtonUI, int>();
        [Header("Settings")]
        [SerializeField]
        private float _fallAnimDuration = 0.3f;
        [SerializeField]
        private Vector3 _punchScaleStrength = new Vector3(0.3f, -0.2f, 0);
        [SerializeField]
        private float _punchDuration = 0.5f;

        private RectTransform _rectTransform;
        private Vector2 _originalAnchoredPosition;
        [SerializeField]
        private Vector3 _choiceMadeMaxScale = new Vector3(0.2f, 0.2f, 0);
        [SerializeField]
        private float _choiceMadeDuration = 0.5f;
        [SerializeField]
        private Ease _choiceMadeEase = Ease.Linear;
        [SerializeField]
        private float _choiceMadeDecreaseDuration = 0.3f;
        [SerializeField]
        private Ease _choiceMadeDecreaseEase = Ease.Linear;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _originalAnchoredPosition = _rectTransform.anchoredPosition;
        }

        private void Start()
        {
            foreach (Transform child in _container)
            {
                Destroy(child.gameObject);
            }

            SessionParams[] sessionParams = _sessionParamsStorage.SessionParams;

            for (int i = 0, count = sessionParams.Length; i < count; i++)
            {
                ButtonUI button = Instantiate(_buttonPrefab, _container);
                button.SetText(sessionParams[i].Id);
                button.OnClick += OnButtonClicked;
                _buttonToParamsMap.Add(button, i);
            }

            LaunchAppearanceAnimation();
        }

        [Button]
        private void LaunchAppearanceAnimation()
        {
            UiUtils.SetPivot(_rectTransform, new Vector2(0.5f, 0f));
            DOTween.Kill(_rectTransform);
            _rectTransform.localScale = Vector3.one;
            float screenHeight = Screen.height;
            float halfHeight = _rectTransform.rect.height / 2;
            _rectTransform.anchoredPosition = new Vector2(_originalAnchoredPosition.x, screenHeight + halfHeight);
            Sequence s = DOTween.Sequence();

            s.Append(_rectTransform.DOAnchorPos(_originalAnchoredPosition, _fallAnimDuration).SetEase(Ease.InQuad));
            s.Append(_rectTransform.DOPunchScale(_punchScaleStrength, _punchDuration, 10, 1f));
            s.Join(_rectTransform.DOAnchorPosY(_originalAnchoredPosition.y + 15f, _punchDuration / 2)
                                 .SetLoops(2, LoopType.Yoyo)
                                 .SetEase(Ease.OutQuad));
        }

        private void OnButtonClicked(ButtonUI buttonUI)
        {
            buttonUI.OnClick -= OnButtonClicked;
            int paramsIndex = _buttonToParamsMap[buttonUI];
            GameParamsService.Instance.SessionParams = _sessionParamsStorage.SessionParams[paramsIndex];
            LaunchDisappearSequence().AppendCallback(LoadNextScene);
        }

        [Button]
        private Sequence LaunchDisappearSequence()
        {
            UiUtils.SetPivot(_rectTransform, new Vector2(0.5f, 0.5f));

            return DOTween.Sequence()
                          .Append(_rectTransform.DOScale(_choiceMadeMaxScale, _choiceMadeDuration).SetEase(_choiceMadeEase))
                          .Append(_rectTransform.DOScale(Vector2.zero, _choiceMadeDecreaseDuration).SetEase(_choiceMadeDecreaseEase));
        }

        private void LoadNextScene()
        {
            SceneManager.LoadScene((int)Scene.Gameplay, LoadSceneMode.Single);
        }

#if UNITY_EDITOR
        [Button]
        private void RestoreScale()
        {
            _rectTransform.localScale = Vector3.one;
        }
#endif
    }
}
