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
        private Volume _volume;
        [SerializeField]
        private float _vignetteAnimDuration = 0.3f;
        [SerializeField]
        private float _vignetteMaxValue = 0.5f;
        [SerializeField]
        private float _chromAbbEndVal = 0.4f;
        [SerializeField]
        private float _chromAbIncreaseDurationRatio = 0.2f;

        private Vignette _vignette;
        private ChromaticAberration _chromaticAberration;
        private Bloom _bloom;
        private Tween _activeVignetteTween;
        private Tween _activeChromAbTween;

        private void Awake()
        {
            if (_volume.profile.TryGet(out _vignette))
            {
                Debug.Log("Vignette found and ready!");
            }
            else
            {
                Debug.LogError("Vignette not found!");
            }

            if (_volume.profile.TryGet(out _chromaticAberration))
            {
                Debug.Log("chromatic abberation found and ready!");
            }
            else
            {
                Debug.LogError("chromatic abberation not found!");
            }
            
            if (_volume.profile.TryGet(out _bloom))
            {
                Debug.Log("bloom found and ready!");
            }
            else
            {
                Debug.LogError("bloom not found!");
            }
        }

        public void FadeVignette01(float targetValue)
        {
            if (_activeVignetteTween != null
                && _activeVignetteTween.IsActive())
            {
                _activeVignetteTween.Kill();
            }

            targetValue = Mathf.Lerp(0, _vignetteMaxValue, targetValue);
            _activeVignetteTween = DOTween.To(() => _vignette.intensity.value, x => _vignette.intensity.value = x, targetValue
                , _vignetteAnimDuration);
        }

        public void LaunchAndFadeChromaticAbberation(float duration)
        {
            if (_activeChromAbTween != null
                && _activeChromAbTween.IsActive())
            {
                _activeChromAbTween.Kill(true);
            }

            DOGetter<float> getter = () => _chromaticAberration.intensity.value;
            DOSetter<float> setter = x => _chromaticAberration.intensity.value = x;
            _activeChromAbTween = DOTween.Sequence()
                                         .Append(DOTween.To(getter, setter, _chromAbbEndVal, duration * _chromAbIncreaseDurationRatio)
                                                        .SetEase(Ease.OutExpo))
                                         .Append(DOTween.To(getter, setter, 0, duration * (1 - _chromAbIncreaseDurationRatio))
                                                        .SetEase(Ease.InQuart));
        }

        public void SetBloomIntensity(float value)
        {
            _bloom.intensity.value = value;
        }
    }
}
