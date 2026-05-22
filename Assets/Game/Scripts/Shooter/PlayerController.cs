using TriInspector;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.Shooter
{
    public sealed class PlayerController : NetworkBehaviour
    {
        [SerializeField]
        [RequiredGet]
        private Player _player;

        public void SetDirection(InputAction.CallbackContext context)
        {
            if (IsOwner)
            {
                var direction = context.ReadValue<Vector2>();
                _player.SetDirection(new Vector3(direction.x, 0, direction.y));
            }
        }

        public void Rotate(InputAction.CallbackContext context)
        {
            if (IsOwner)
            {
                var direction = context.ReadValue<Vector2>();
                _player.SetRotationDirection(new Vector3(0, direction.x, 0));
            }
        }

        public void Shoot(InputAction.CallbackContext context)
        {
            if (context.performed && IsOwner)
            {
                Debug.Log($"<color=green>Shoot input</color>");
                _player.Shoot();
            }
        }

        public void Aim(InputAction.CallbackContext context)
        {
            if (context.started && IsOwner)
            {
                _player.Aim();
            }
            else if (context.canceled && IsOwner)
            {
                _player.CancelAim();
            }
        }
    }
}
