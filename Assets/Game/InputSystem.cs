using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public sealed class InputSystem : MonoBehaviour, InputSystem_Actions.IPlayerActions
    {
        public event Action OnSwipeStarted;
        public event Action OnSwipeCancelled;
        public float PointerPositionX { get; private set; }
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
            if (context.started)
            {
                PointerPositionX = context.ReadValue<float>();
                OnSwipeStarted?.Invoke();
                Debug.Log($"<color=green>swipe start</color>");
            }

            if (context.performed)
            {
                PointerPositionX = context.ReadValue<float>();
            }

            if (context.canceled)
            {
                OnSwipeCancelled?.Invoke();
            }
        }

        private void OnDisable()
        {
            _actions.Player.Disable();
        }
    }
}
