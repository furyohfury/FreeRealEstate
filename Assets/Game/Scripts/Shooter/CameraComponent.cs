using Unity.Cinemachine;
using Unity.Netcode;
using UnityEngine;

namespace Game.Scripts.Shooter
{
    public class CameraComponent : NetworkBehaviour
    {
        public bool IsAiming => _isAiming;

        [SerializeField]
        private Transform _cameraTarget;
        private CinemachineCamera _vcam;
        private bool _isAiming;

        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                _vcam = FindFirstObjectByType<CinemachineCamera>();
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
                _vcam.GetComponent<CinemachinePresetsController>().SetWalkingPreset();
                _isAiming = false;
            }
        }

        public void SetShootingPreset()
        {
            if (IsOwner)
            {
                _vcam.GetComponent<CinemachinePresetsController>().SetShootingPreset();
                _isAiming = true;
            }
        }
    }
}
