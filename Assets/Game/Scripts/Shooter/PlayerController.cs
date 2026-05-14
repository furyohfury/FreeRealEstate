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
    }
}
