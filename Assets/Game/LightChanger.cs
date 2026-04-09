using System;
using TriInspector;
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
        [SerializeField]
        private float _minBloom = 30f;
        [SerializeField]
        private float _maxBloom = 110f;
        [SerializeField]
        private CameraFacade _cameraFacade;

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
            else
            {
                SetTimeOfDay(1);
            }
        }

        private float GetDayCycleValue(DateTime time)
        {
            float totalSeconds = (float)time.TimeOfDay.TotalSeconds;

            var ratio = totalSeconds <= HALF_DAY_SECONDS
                ? Mathf.InverseLerp(0, HALF_DAY_SECONDS, totalSeconds)
                : Mathf.InverseLerp(HALF_DAY_SECONDS * 2, HALF_DAY_SECONDS, totalSeconds);
            Debug.Log($"Day cycle value of {time.ToString("HH:mm:ss zz")}: {ratio}");

            return ratio;
        }

        private void SetTimeOfDay(float ratio)
        {
            _ratio = ratio;
            SetLight(ratio);
            _cameraFacade.SetBloomIntensity(Mathf.Lerp(_maxBloom, _minBloom, ratio));
        }

        private void SetLight(float ratio)
        {
            _light.color = Color.Lerp(_nightColor, _dayColor, ratio);
            _light.intensity = Mathf.Lerp(_minLightIntensity, _maxLightIntensity, ratio);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_isActive)
            {
                SetTimeOfDay(_ratio);
            }
        }

        [Button]
        private void DebugDayTime()
        {
            DateTime dateTime = DateTime.Now;
            var before = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 9, 0, 0);
            var after = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 21, 0, 0);
            // Debug.Log(GetDayCycleValue(before));
            // Debug.Log(GetDayCycleValue(after));
            GetDayCycleValue(before);
            GetDayCycleValue(after);
        }
#endif
    }
}
