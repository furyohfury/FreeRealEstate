using Unity.Netcode;
using Zenject;

namespace Game.Scripts.Shooter
{
    public sealed class AimUIComponent : NetworkBehaviour
    {
        private AimIcon _aimIcon;

        [Inject]
        private void Construct(AimIcon aimIcon)
        {
            _aimIcon = aimIcon;
        }

        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                HideAimUI();
            }
        }

        public void ShowAimUI()
        {
            _aimIcon.gameObject.SetActive(true);
        }

        public void HideAimUI()
        {
            _aimIcon.gameObject.SetActive(false);
        }
    }
}
