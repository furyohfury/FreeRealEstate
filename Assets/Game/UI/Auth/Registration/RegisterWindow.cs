using R3;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
	public sealed class RegisterWindow : MonoBehaviour, IRegisterWindow
	{
		public Observable<Unit> OnRegisterButtonPressed { get; private set; }
		public Observable<Unit> OnBackButtonPressed { get; private set; }
		public Observable<string> Email { get; private set; }
		public Observable<string> Password { get; private set; }
		public Observable<string> Nickname { get; private set; }

		[SerializeField]
		private TMP_InputField _emailInputField;
		[SerializeField]
		private TMP_InputField _passwordInputField;
		[SerializeField]
		private TMP_InputField _nicknameInputField;
		[SerializeField]
		private TMP_Text _errorField;
		[SerializeField]
		private Button _registerButton;
		[SerializeField] [Required]
		private Button _backButton;


		public void SetRegisterButtonInteractable(bool isActive)
		{
			_registerButton.interactable = isActive;
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
			OnRegisterButtonPressed = _registerButton.OnClickAsObservable();
			OnBackButtonPressed = _backButton.OnClickAsObservable();
			Email = _emailInputField.OnValueChangedAsObservable().Skip(1);
			Password = _passwordInputField.OnValueChangedAsObservable().Skip(1);
			Nickname = _nicknameInputField.OnValueChangedAsObservable().Skip(1);
		}
	}
}