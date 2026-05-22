using Game.Scripts.Shooter;
using Unity.Cinemachine;
using Unity.Netcode;
using UnityEngine;

namespace Game.Scripts.ShooterS
{
    public class CameraComponent : NetworkBehaviour
    {
        [SerializeField]
        private Transform _cameraTarget;
        private CinemachineCamera _vcam;

        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                _vcam = FindFirstObjectByType<CinemachineCamera>();
                _vcam.Target = new CameraTarget
                               {
                                   TrackingTarget = _cameraTarget
                               };
            }
        }

        public void SetWalkingPreset()
        {
            if (IsOwner)
            {
                _vcam.GetComponent<CinemachinePresetsController>().SetWalkingPreset();
            }
        }

        public void SetShootingPreset()
        {
            if (IsOwner)
            {
                _vcam.GetComponent<CinemachinePresetsController>().SetShootingPreset();
            }
        }
    }
}
