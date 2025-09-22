using System;
using R3;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
	public sealed class LoginWindow : MonoBehaviour, ILoginWindow
	{
		public Observable<Unit> OnSignInButtonPressed { get; private set; }
		public Observable<Unit> OnBackButtonPressed { get; private set; }
		public Observable<string> Email { get; private set; }
		public Observable<string> Password { get; private set; }

		[SerializeField] [Required]
		private TMP_InputField _emailInputField;
		[SerializeField] [Required]
		private TMP_InputField _passwordInputField;
		[SerializeField] [Required]
		private TMP_Text _errorField;
		[SerializeField] [Required]
		private Button _signInButton;
		[SerializeField] [Required]
		private Button _backButton;

		public void SetLoginButtonInteractable(bool isActive)
		{
			_signInButton.interactable = isActive;
		}

		public void SetPasswordField(string text)
		{
			_passwordInputField.text = text;
		}

		public void SetErrorField(string text)
		{
			_errorField.SetText(text);
		}

		public void Close()
		{
			Destroy(gameObject);
		}

		private void Awake()
		{
			OnSignInButtonPressed = _signInButton.OnClickAsObservable();
			OnBackButtonPressed = _backButton.OnClickAsObservable();
			Email = _emailInputField.OnValueChangedAsObservable().Skip(1);
			Password = _passwordInputField.OnValueChangedAsObservable().Skip(1);
		}
	}
}