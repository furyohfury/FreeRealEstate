using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public sealed class InputSystem : Singleton<InputSystem>, InputSystem_Actions.IPlayerActions
    {
        public event Action OnSwipeStarted;
        public event Action OnSwipeCancelled;
        public event Action OnDragStarted;
        public event Action OnDragCancelled;
        public float PointerPositionX { get; private set; }
        private InputSystem_Actions _actions;

        protected override void Awake()
        {
            base.Awake();
            _actions = new InputSystem_Actions();
            _actions.Player.SetCallbacks(this);
        }

        private void OnEnable()
        {
            Enable();
        }

        public void Enable()
        {
            _actions.Player.Enable();
        }

        public void OnSwipeItem(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                PointerPositionX = context.ReadValue<float>();
                OnSwipeStarted?.Invoke();
                // Debug.Log($"<color=green>swipe start</color>");
            }

            if (context.performed)
            {
                PointerPositionX = context.ReadValue<float>();
            }

            if (context.canceled)
            {
                OnSwipeCancelled?.Invoke();
                // Debug.Log($"<color=red>swipe cancel</color>");
            }
        }

        public void OnDragItem(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _actions.Player.SwipeItem.Disable();
                OnDragStarted?.Invoke();
                Debug.Log($"<color=green>drag performed</color>");
            }
            if (context.canceled)
            {
                _actions.Player.SwipeItem.Enable();
                OnDragCancelled?.Invoke();
                Debug.Log("<color=red>drag cancel</color>");
            }
        }

        private void OnDisable()
        {
            Disable();
        }

        public void Disable()
        {
            _actions.Player.Disable();
        }
    }
}
