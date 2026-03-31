using DG.Tweening;
using TMPro;
using TriInspector;
using UnityEngine;

namespace Game
{
    public sealed class CountDownUI : Singleton<CountDownUI>
    {
        [SerializeField]
        private TextMeshProUGUI _countdownText;
        private Vector3 _initialScale;
        [SerializeField]
        private Vector3 _minimalScale = new Vector3(0.1f, 0.1f, 1f);
        [SerializeField]
        private float _decreaseDuration = 0.9f;
        private Tween _tween;
        [SerializeField]
        private float _finalMessageAppearanceDuration = 2f;
        [SerializeField]
        private string _countdownFinalText = "GO!";

        protected override void Awake()
        {
            base.Awake();
            _countdownText.enabled = false;
            _initialScale = _countdownText.transform.localScale;
        }

        [Button]
        public async Awaitable LaunchCountdown(int startVal)
        {
            _countdownText.enabled = true;
            _countdownText.transform.localScale = _initialScale;

            while (startVal > 0)
            {
                _countdownText.text = startVal.ToString();
                _tween?.Kill();
                _tween = _countdownText.transform.DOScale(_minimalScale, _decreaseDuration);

                await Awaitable.WaitForSecondsAsync(_decreaseDuration);

                startVal--;

                if (startVal > 0)
                {
                    _tween?.Kill();
                    _tween = _countdownText.transform.DOScale(_initialScale, 1 - _decreaseDuration);

                    await Awaitable.WaitForSecondsAsync(1 - _decreaseDuration);
                }
                else
                {
                    break;
                }
            }

            _countdownText.text = _countdownFinalText;
            _tween?.Kill();
            _countdownText.transform.DOScale(_initialScale, 0.2f).SetEase(Ease.OutBack);

            await Awaitable.WaitForSecondsAsync(_finalMessageAppearanceDuration);

            _countdownText.enabled = false;
        }
    }
}
