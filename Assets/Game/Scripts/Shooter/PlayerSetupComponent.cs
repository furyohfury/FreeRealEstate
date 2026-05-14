using TriInspector;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.Shooter
{
    public class PlayerSetupComponent : NetworkBehaviour
    {
        [SerializeField]
        [RequiredGet]
        private PlayerInput playerInput;

        public override void OnNetworkSpawn()
        {
            // Если это НЕ наш локальный игрок, выключаем ему ввод
            if (!IsOwner)
            {
                playerInput.enabled = false;
            }
        }
    }
}
