using UnityEngine;

namespace Game
{
    public sealed class CameraProvider : Singleton<CameraProvider>
    {
        public CameraFacade CameraFacade => _cameraFacade;

        [SerializeField]
        private CameraFacade _cameraFacade;
    }
}