using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.Shooter
{
    public sealed class TakeDamageDebugHelper : MonoBehaviour
    {
        [SerializeField]
        private Player _player;
        private void Update()
        {
            if (Keyboard.current.tKey.wasPressedThisFrame)
            {
                _player.TakeDamage(1);
            }
        }
    }
}
