using UnityEngine;

namespace Game
{
    public sealed class CameraFacade : MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;
        [SerializeField]
        private CameraShaker _shaker;

        public void ShakeCamera()
        {
            _shaker.Shake();
        }
    }
}
