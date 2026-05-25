using Unity.Netcode;

namespace Game.Scripts.Shooter
{
    public sealed class AimUIComponent : NetworkBehaviour
    {
        private AimIcon _aimIcon;

        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                _aimIcon = FindAnyObjectByType<AimIcon>();
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
