using System;
using Game.Application;
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
        private CameraFacade _cameraFacade;

        private bool _isDay;

        private void Awake()
        {
            AppConfiguration appConfiguration = AppConfigurationProvider.Instance.Configuration;
            _minLightIntensity = appConfiguration.GetNightMainLightIntensity();
            _maxLightIntensity = appConfiguration.GetDayMainLightIntensity();
        }

        private void Start()
        {
            DateTime now = DateTime.Now;

            if (now != default(DateTime))
            {
                int hour = now.Hour;

                if (hour >= 6
                    && hour <= 18)
                {
                    SwitchToDay();
                }
                else
                {
                    SwitchToNight();
                }
            }
            else
            {
                SwitchToDay();
            }
        }

        public void SwitchToOpposite()
        {
            if (_isDay)
            {
                SwitchToNight();
            }
            else
            {
                SwitchToDay();
            }
        }

        private void SetLight(float ratio)
        {
            _light.color = Color.Lerp(_nightColor, _dayColor, ratio);
            _light.intensity = Mathf.Lerp(_minLightIntensity, _maxLightIntensity, ratio);
        }

        [Button]
        private void SwitchToDay()
        {
            SetLight(1);
            _cameraFacade.SetDayPP();
            _isDay = true;
            Debug.Log("Day is now " + _isDay);
        }

        [Button]
        private void SwitchToNight()
        {
            SetLight(0);
            _cameraFacade.SetNightPP();
            _isDay = false;
            Debug.Log("Day is now " + _isDay);
        }
    }
}
