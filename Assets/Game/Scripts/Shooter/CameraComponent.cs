using Unity.Cinemachine;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Shooter
{
    public class CameraComponent : NetworkBehaviour
    {
        public bool IsAiming => _isAiming;

        [SerializeField]
        private Transform _cameraTarget;
        private CinemachineCamera _vcam;
        private bool _isAiming;

        [Inject]
        public void Construct(CinemachineCamera cam)
        {
            _vcam = cam;
        }

        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                _vcam.Target = new CameraTarget
                               {
                                   TrackingTarget = _cameraTarget
                               };

                SetWalkingPreset();
            }
        }

        public void SetWalkingPreset()
        {
            if (IsOwner)
            {
                _vcam.GetComponent<CinemachinePresetsController>()
                     .SetWalkingPreset();
                _isAiming = false;
            }
        }

        public void SetShootingPreset()
        {
            if (IsOwner)
            {
                _vcam.GetComponent<CinemachinePresetsController>()
                     .SetShootingPreset();
                _isAiming = true;
            }
        }
    }
}
