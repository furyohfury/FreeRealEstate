using UnityEngine;

namespace Game
{
    public sealed class LowHpObserver : MonoBehaviour
    {
        private void Start()
        {
            Health.Instance.OnHealthChanged += InstanceOnOnHealthChanged;
        }

        private void InstanceOnOnHealthChanged(float hp)
        {
            CameraFacade cameraFacade = CameraProvider.Instance.CameraFacade;
            float vignetteRatio = 1 - hp / Health.Instance.MaxHealth;
            cameraFacade.SetVignette(vignetteRatio);
        }

        private void OnDestroy()
        {
            Health.Instance.OnHealthChanged -= InstanceOnOnHealthChanged;
        }
    }
}
