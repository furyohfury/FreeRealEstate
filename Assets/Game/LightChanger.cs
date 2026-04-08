using System;
using UnityEngine;

namespace Game
{
    public class LightChanger : MonoBehaviour
    {
        [SerializeField]
        [Range(0, 1)]
        private float _ratio;
        [SerializeField]
        private Light _light;
        [SerializeField]
        private Color _dayColor;
        [SerializeField]
        private Color _nightColor;
        [SerializeField]
        private float _minLightIntensity;
        [SerializeField]
        private float _maxLightIntensity;
        [SerializeField]
        private bool _isActive = true;

        private const float HALF_DAY_SECONDS = 43200f;

        private void Start()
        {
            DateTime now = DateTime.Now;

            if (now != default(DateTime))
            {
                float dayCycleValue = GetDayCycleValue(now);
                SetTimeOfDay(dayCycleValue);
                Debug.Log($"System time is {now.Hour}:{now.Minute}. Set light for scene at {dayCycleValue} value");
            }
        }

        private float GetDayCycleValue(DateTime time)
        {
            float totalSeconds = (float)time.TimeOfDay.TotalSeconds;
            float secondsInHalfCycle = totalSeconds % HALF_DAY_SECONDS;

            return secondsInHalfCycle / HALF_DAY_SECONDS;
        }

        private void SetTimeOfDay(float ratio)
        {
            _light.color = Color.Lerp(_dayColor, _nightColor, ratio);
            _light.intensity = Mathf.Lerp(_maxLightIntensity, _minLightIntensity, ratio);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_isActive)
            {
                SetTimeOfDay(_ratio);
            }
        }
#endif
    }
}
