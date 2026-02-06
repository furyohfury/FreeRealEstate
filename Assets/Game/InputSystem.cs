using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public sealed class InputSystem : MonoBehaviour, InputSystem_Actions.IPlayerActions
    {
        private InputSystem_Actions _actions;

        private void Awake()
        {
            _actions = new InputSystem_Actions();
            _actions.Player.SetCallbacks(this);
        }

        private void OnEnable()
        {
            _actions.Player.Enable();
        }

        public void OnMoveItem(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                float xAxisValue = context.ReadValue<float>();
                Debug.Log($"xAxisValue: {xAxisValue}");
            }
        }

        private void OnDisable()
        {
            _actions.Player.Disable();
        }
    }
}
