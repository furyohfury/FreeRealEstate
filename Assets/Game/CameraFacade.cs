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
        public void ShakeCamera()
        {
            _shaker.Shake();
        }

        [Button]
        public void SetVignette(float value)
        {
            _postProcessController.FadeVignette01(value);
        }
    }
}
