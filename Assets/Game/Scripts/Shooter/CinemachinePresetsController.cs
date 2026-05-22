using Unity.Netcode;
using UnityEngine;

namespace Game.Scripts.Shooter
{
    public sealed class CinemachinePresetsController : NetworkBehaviour
    {
        [SerializeField]
        private CinemachineFollowPreset _shootingPreset;
        [SerializeField]
        private CinemachineFollowPreset _walkingPreset;

        public void SetWalkingPreset()
        {
            _walkingPreset.Load();
        }

        public void SetShootingPreset()
        {
            _shootingPreset.Load();
        }
    }
}
