using TriInspector;
using UnityEngine;

namespace Game
{
    public sealed class CameraFacade : MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;
        [SerializeField]
        private CameraShaker _shaker;
        [SerializeField]
        private PostProcessController _postProcessController;

        [Button]
        public void ShakeCamera(float duration)
        {
            _shaker.Shake(duration);
        }

        [Button]
        public void SetVignette(float value)
        {
            _postProcessController.FadeVignette01(value);
        }

        [Button]
        public void LaunchChromaticAbberation(float duration)
        {
            _postProcessController.LaunchAndFadeChromaticAbberation(duration);
        }

        public void SetBloomIntensity(float value)
        {
            _postProcessController.SetBloomIntensity(value);
        }
    }
}
