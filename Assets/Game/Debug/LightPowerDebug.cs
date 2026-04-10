using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class LightPowerDebug : MonoBehaviour
    {
        [SerializeField]
        private Slider _slider;
        [SerializeField]
        private TMP_Text _text;
        [SerializeField]
        private float _minIntensity;
        [SerializeField]
        private float _maxIntensity;

        private Light _mainLight;

        private void Awake()
        {
            _mainLight = FindFirstObjectByType<Light>();
        }

        private void OnEnable()
        {
            _slider.value = Mathf.InverseLerp(_minIntensity, _maxIntensity, _mainLight.intensity);
            _slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        private void OnSliderValueChanged(float arg0)
        {
            _mainLight.intensity = Mathf.Lerp(_minIntensity, _maxIntensity, arg0);
            _text.text = $"{arg0}";
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }
    }
}
