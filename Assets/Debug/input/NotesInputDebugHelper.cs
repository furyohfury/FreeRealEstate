using System;
using Game.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameDebug.input
{
	public class NotesInputDebugHelper : MonoBehaviour
	{
		private InputActions _inputActions;

		private void Awake()
		{
			_inputActions = new InputActions();
			_inputActions.Enable();

			_inputActions.Player.Blue.performed += OnBlue;
			Debug.Log("awake");
		}

		private void OnBlue(InputAction.CallbackContext obj)
		{
			Debug.Log("Blue pressed");
		}

		private void OnDestroy()
		{
			_inputActions.Player.Blue.performed -= OnBlue;
		}
	}
}