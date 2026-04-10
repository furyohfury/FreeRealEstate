using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Game
{
    public sealed class PostProcessController : MonoBehaviour
    {
        [SerializeField]
        private Volume _dayVolume;
        [SerializeField]
        private Volume _nightVolume;
        [SerializeField]
        private Volume _persistentVolume;
        [SerializeField]
        private float _vignetteAnimDuration = 0.3f;
        [SerializeField]
        private float _vignetteMaxValue = 0.5f;
        [SerializeField]
        private float _chromAbbEndVal = 0.4f;
        [SerializeField]
        private float _chromAbIncreaseDurationRatio = 0.2f;

        private Tween _activeVignetteTween;
        private Tween _activeChromAbTween;

        private void Awake()
        {
            _dayVolume.weight = 1f;
            _nightVolume.weight = 0f;
        }

        public void FadeVignette01(float targetValue)
        {
            if (_activeVignetteTween != null
                && _activeVignetteTween.IsActive())
            {
                _activeVignetteTween.Kill();
            }

            if (_persistentVolume.profile.TryGet(out Vignette vignette))
            {
                targetValue = Mathf.Lerp(0, _vignetteMaxValue, targetValue);
                _activeVignetteTween = DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, targetValue,
                    _vignetteAnimDuration);
            }
        }

        public void LaunchAndFadeChromaticAbberation(float duration)
        {
            if (_activeChromAbTween != null
                && _activeChromAbTween.IsActive())
            {
                _activeChromAbTween.Kill(true);
            }

            if (_persistentVolume.profile.TryGet(out ChromaticAberration cromAb))
            {
                DOGetter<float> getter = () => cromAb.intensity.value;
                DOSetter<float> setter = x => cromAb.intensity.value = x;
                _activeChromAbTween = DOTween.Sequence()
                                             .Append(DOTween.To(getter, setter, _chromAbbEndVal, duration * _chromAbIncreaseDurationRatio)
                                                            .SetEase(Ease.OutExpo))
                                             .Append(DOTween.To(getter, setter, 0, duration * (1 - _chromAbIncreaseDurationRatio))
                                                            .SetEase(Ease.InQuart));
            }
        }

        public void SetDayPP()
        {
            _dayVolume.weight = 1f;
            _nightVolume.weight = 0f;
        }

        public void SetNightPP()
        {
            _nightVolume.weight = 1f;
            _dayVolume.weight = 0f;
        }
    }
}
