using Unity.Cinemachine;
using Unity.Netcode;

namespace Game.Scripts.Shooter
{
    public class CameraComponent : NetworkBehaviour
    {
        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                var vcam = FindFirstObjectByType<CinemachineCamera>();
                vcam.Target = new CameraTarget
                              {
                                  TrackingTarget = transform
                              };
            }
        }
    }
}
