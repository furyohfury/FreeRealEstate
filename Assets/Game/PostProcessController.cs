using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
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
        private float _duration = 0.3f;
        [SerializeField]
        private float _maxValue = 0.5f;

        private Vignette _vignette;
        private TweenerCore<float, float, FloatOptions> _activeTween;

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
        }

        public void FadeVignette01(float targetValue)
        {
            if (_activeTween != null
                && _activeTween.IsActive())
            {
                _activeTween.Kill();
            }

            targetValue = Mathf.Lerp(0, _maxValue, targetValue);
            _activeTween = DOTween.To(() => _vignette.intensity.value, x => _vignette.intensity.value = x, targetValue, _duration);
        }
    }
}
